using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    public enum eState
    {
        /// <summary>
        /// 가만히 있는 상태
        /// </summary>
        Idle,
        /// <summary>
        /// 적을 쫓다가 일정거리 이상 벗어나서 원래 위치로 돌아가는 상태
        /// </summary>
        Flee,
        /// <summary>
        /// 정찰/순찰 상태
        /// </summary>
        Patrol,
        /// <summary>
        /// 적을 발견하여 쫓는 상태
        /// </summary>
        Chase,
        /// <summary>
        /// 적을 공격하는 상태
        /// </summary>
        Attack,
        /// <summary>
        /// 사망 상태
        /// </summary>
        Dead,
    }

    public eState curState = eState.Idle;
    public NavMeshAgent aiAgent;
    private Vector3 _startPos;
    private Character _target;

    private void Update()
    {
        OnUpdateState(curState);
        animator.SetBool("isMoving", aiAgent.velocity.magnitude > 0);
    }

    void Start()
    {
        OnEnterState(eState.Idle);
    }

    /// <summary>
    /// 상태를 변경합니다.
    /// </summary>
    /// <param name="targetState">변경 하려는 상태</param>
    /// <param name="isDuplicatable">같은 상태로 변경될 때 로직을 돌릴것인가?</param>
    public void ChangeState(eState targetState, bool isDuplicatable = false)
    {
        if(isDuplicatable == false && curState == targetState)
        {
            return;
        }
        OnExitState(curState);
        OnEnterState(targetState);
        curState = targetState;
    }

    private void OnUpdateState(eState targetState)
    {
        switch (targetState)
        {
            case eState.Idle:
                {
                    //초기화
                    break;
                }
            case eState.Flee:
                {
                    break;
                }
            case eState.Patrol:
                {
                    Collider[] targets = Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("Player"));
                    if (targets != null && targets.Length > 0)//적인식
                    {
                        _target = targets[0].GetComponent<Character>();
                        ChangeState(eState.Chase);
                    }
                    break;
                }
            case eState.Chase:
                {
                    if (_target != null)
                    {
                        aiAgent.destination = _target.transform.position;
                    }
                    break;
                }
            case eState.Attack:
                {
                    //Attack OnUpdateState
                    break;
                }
            case eState.Dead:
                {
                    //Dead OnUpdateState
                    break;
                }
        }
    }

    private void OnExitState(eState targetState)
    {
        switch (targetState)
        {
            case eState.Idle:
                {
                    //Idle OnExitState
                    break;
                }
            case eState.Flee:
                {
                    //Flee OnExitState
                    break;
                }
            case eState.Patrol:
                {
                    StopCoroutine(Patrolling());
                    break;
                }
            case eState.Chase:
                {
                    //Chase OnExitState
                    break;
                }
            case eState.Attack:
                {
                    //Attack OnExitState
                    break;
                }
            case eState.Dead:
                {
                    //Dead OnExitState
                    break;
                }
        }
    }

    private void OnEnterState(eState targetState)
    {
        switch (targetState)
        {
            case eState.Idle:
                {
                    aiAgent.speed = this.moveSpeed;
                    aiAgent.angularSpeed = 720;
                    aiAgent.stoppingDistance = 2;
                    aiAgent.acceleration = 10;
                    aiAgent.isStopped = true;
                    _startPos = transform.position;
                    StartCoroutine(DelayChangeState(eState.Patrol, 2));
                    break;
                }
            case eState.Flee:
                {
                    //Flee EnterState
                    break;
                }
            case eState.Patrol:
                {
                    StartCoroutine(Patrolling());
                    break;
                }
            case eState.Chase:
                {
                    if (_target == null)
                    {
                        ChangeState(eState.Flee);
                        return;
                    }
                    aiAgent.isStopped = false;
                    aiAgent.destination = _target.transform.position;
                    break;
                }
            case eState.Attack:
                {
                    //Attack EnterState
                    break;
                }
            case eState.Dead:
                {
                    //Dead EnterState
                    break;
                }
        }
    }

    private IEnumerator Patrolling()
    {
        while(true)
        {
            aiAgent.isStopped = false;
            Vector3 _offset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            aiAgent.destination = _startPos + _offset;
            yield return new WaitForSeconds(Random.Range(2f,6f));
        }
    }


    private IEnumerator DelayChangeState(eState targetState, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(targetState);
    }
}
