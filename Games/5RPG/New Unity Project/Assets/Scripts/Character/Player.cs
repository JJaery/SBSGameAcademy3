using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventType = AnimationEventListener.EventType;

public class Player : Character
{
    public Rigidbody rigidBody;
    public AnimationEventListener animaitionListener;
    public bool isMovable = true, isAttackable = true;

    private void Awake()
    {
        animaitionListener.onAnimationEvent += OnAnimationEvent;
    }
    
    private void OnDestroy()
    {
        animaitionListener.onAnimationEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(EventType eventName)
    {
        switch (eventName)
        {
            case EventType.MeleeAttackStart:
                {
                    isMovable = false;
                    isAttackable = false;
                    break;
                }
            case EventType.MeleeAttackEnd:
                {
                    isMovable = true;
                    isAttackable = true;
                    break;
                }
            case EventType.OnAttack:
                {
                    OnAttack();
                    break;
                }
        }
    }

    private void OnAttack()
    {
        //주변에 있는 적 검색 -> 데미지 주기 -> 끗
        List<Monster> enemies = GetEnemies();
        foreach(Monster enemy in enemies)
        {
            Hit(enemy);
        }
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isAttackable == true)
            {
                isAttackable = false;
                Attack();
            }
            return;
        }

        if (isMovable == true)
        {
            Move();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("onAttack");
    }

    private void Move()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W) == true) // 방향이 0도
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
            rigidBody.position += animator.transform.forward * moveSpeed * Time.deltaTime;
            animator.transform.rotation =
                Quaternion.RotateTowards(
                    animator.transform.rotation,
                    Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, dir, Vector3.up), 0)
                , 720 * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else // 이동 하지 않을 때 
        {
            animator.SetBool("isMoving", false);
        }
    }

    private List<Monster> GetEnemies()
    {
        List<Monster> result = new List<Monster>();
        //원안의 오브젝트들을 가져오기 위해서 Physics 사용
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 2, LayerMask.GetMask("Monster"));
        if (colliders != null)
        {
            foreach(Collider target in colliders)
            {
                //1. direction(플레이어<->타겟을 기준으로한 방향) 을 구해야 합니다.
                Vector3 direction = target.transform.position - transform.position;
                //float distance = direction.magnitude;
                if( Vector3.Angle(animator.transform.forward, direction) <= 45 * 0.5f)
                {
                    Monster script = target.GetComponent<Monster>();
                    if(script != null)
                    {
                        result.Add(script);
                    }
                }
            }
        }
        return result;
    }

    //씬뷰에서만 보이는 그림 그릴 때 사용하는 메소드
    private void OnDrawGizmos()
    {
        //씬뷰에서만 보이는 그림을 그리는 역할을 담당하는 클래스, => Handle
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, animator.transform.forward, 45 / 2, 2);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, animator.transform.forward, -45 / 2, 2);
    }
}
