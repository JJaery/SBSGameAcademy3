using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //gordo의 애니메이터
    public Animator _targetAnimator;
    public float moveSpeed = 10;

    private void Awake()
    {
        _targetAnimator.SetTrigger("Idle_01");
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _targetAnimator.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            _targetAnimator.transform.localScale = new Vector3(-0.25f, 0.25f, 1f);
        }
    }

    private void Move()
    {
        _targetAnimator.ResetTrigger("Run_01");
        _targetAnimator.ResetTrigger("Idle_01");

        if (Input.GetKey(KeyCode.A) == true)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);
            _targetAnimator.SetTrigger("Run_01");
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            _targetAnimator.SetTrigger("Run_01");
        }

        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            _targetAnimator.SetTrigger("Idle_01");
        }
    }
}
