using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SpawnManager>();
            return _instance;
        }
    }
    private static SpawnManager _instance;


    public Transform[] wayPoints;
    public GameObject currentSpawnTarget;
    public float spawnRate = 1f;

    public void Awake()
    {
        currentSpawnTarget = Resources.Load<GameObject>("Monster");
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            GameObject obj = Instantiate(currentSpawnTarget);
            obj.transform.position = wayPoints[0].position;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
