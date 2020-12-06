using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform startSpawnPoint, endSpawnPoint;

    public GameObject dungPrefab;

    /// <summary>
    /// 영역의 넓이와 높이
    /// </summary>
    private float width, height;

    /// <summary>
    /// 시작 시 장애물이 스폰되는 시간
    /// </summary>
    public float startInterval = 3.0f;

    /// <summary>
    /// 해당 시간 마다 addStep만큼 속도를 증가시킬 겁니다.
    /// </summary>
    public float addIntervalTime = 5f;
    public float addStep = 0.1f;

    public int currentSpawnCount = 1;
    public float addSpawnIntervalTime = 3f;

    public void Awake()
    {
        // 1. 영역의 넓이
        // 2. 영역의 높이
        // 3. 생성 코루틴 실행
        width = endSpawnPoint.position.x - startSpawnPoint.position.x;
        height = endSpawnPoint.position.y - startSpawnPoint.position.y;

        StartCoroutine(Spawn());
        StartCoroutine(balCoroutine());
        StartCoroutine(balCountCoroutine());
    }


    /// <summary>
    /// 스폰 수량 늘림
    /// </summary>
    /// <returns></returns>
    IEnumerator balCountCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(addSpawnIntervalTime);
            currentSpawnCount += 1;
        }
    }

    /// <summary>
    /// 스폰 시간 줄어듬
    /// </summary>
    /// <returns></returns>
    IEnumerator balCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(addIntervalTime);
            startInterval -= addStep;
            startInterval = Mathf.Max(startInterval, 0.1f); // 최소 값 지정
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i < currentSpawnCount; i++)
            {
                Vector3 randomPoint = new Vector3(Random.Range(0f, width), Random.Range(0f, height));
                Vector3 spawnPoint = startSpawnPoint.transform.position + randomPoint;

                GameObject obj = Instantiate(dungPrefab);
                obj.transform.position = spawnPoint;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
            yield return new WaitForSeconds(startInterval);
        }
    }
}
