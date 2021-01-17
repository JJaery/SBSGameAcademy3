using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody rigidBody;
    public Animator animator;
    public AnimationEventListener animaitionListener;

    private void Awake()
    {
        animaitionListener.onAnimationEvent += OnAnimationEvent;
    }
    private void OnDestroy()
    {
        animaitionListener.onAnimationEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            return;
        }

        Vector3 dir = Vector3.zero;
        if(Input.GetKey(KeyCode.W) == true) // 방향이 0도
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A) == true) // 방향이 -90도
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) == true) //방향이 90도
        {
            dir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S) == true) // 방향이 180도 설정
        {
            dir += Vector3.back;
        }
        ///이동
        if (dir != Vector3.zero)
        {
            rigidBody.position += dir.normalized * moveSpeed * Time.deltaTime;
            animator.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, dir, Vector3.up), 0);
            animator.SetBool("isMoving", true);
        }
        else // 이동 하지 않을 때 
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MeleeAttackStart()
    {
        Debug.Log("!!");
    }

    private void Attack()
    {
        animator.SetTrigger("onAttack");
    }


}
