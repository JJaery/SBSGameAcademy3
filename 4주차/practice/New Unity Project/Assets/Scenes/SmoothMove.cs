using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    private void OnEnable()
    {
        //StartCoroutine(CoMove());
        Move();
    }


    IEnumerator CoMove()
    {
        int i = 0;

        while (i++ < 10)
        {
            transform.position += Vector3.right;
            yield return null;
        }
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
