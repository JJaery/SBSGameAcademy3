using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물 생성과 아이템 생성을 담당하는 매니저
/// </summary>
public class SpawnManager : MonoBehaviour
{
    ///1. 장애물 원본(프리팹)
    public List<GameObject> hurdlePrefab;

    public Transform backgroundTransform;

    //코루틴을 사용할 때 주의할점, 절대로 Update 안에서 StartCoroutine을 하면 안됩니다.

    private void Start()
    {
        StartCoroutine(MakeRoutine());
    }


    IEnumerator MakeRoutine()
    {
        while(true)
        {
            /// Index를 랜덤하게 설정
            int index = Random.Range(0, hurdlePrefab.Count);
            // hurdlePrefab 복제하고 배치
            GameObject copiedObj = Instantiate(hurdlePrefab[index], backgroundTransform);
            copiedObj.transform.position = new Vector3(10, -1f, 0);

            float scale = Random.Range(0.1f, 1.5f);
            copiedObj.transform.localScale *= scale;

            //copiedObj.transform.parent = backgroundTransform;
            //int result = Random.Range(1, 3 + 1);
            float result2 = Random.Range(1f, 3f);
            yield return new WaitForSeconds(result2);
        }
    }

}
