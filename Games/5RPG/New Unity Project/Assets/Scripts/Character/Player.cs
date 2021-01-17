using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody rigidBody;
    public Animator animator;

    private void Update()
    {
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
}
