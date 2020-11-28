using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    float curTime = 0;
    private void Update()
    {
        if (curTime >= 1f)
        {
            Move();
            curTime = 0;
        }
        else
            curTime += Time.deltaTime;
    }
    void Move()
    {
        int i = 0;

        while(i++ < 10)
        {
            transform.position += Vector3.right;
        }
    }
}
