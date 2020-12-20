using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Monster currentTarget = null;
    public float attackRange = 2f;
    public float attackSpeed = 1f;
    public GameObject missile = null;

    public void Start()
    {
        StartCoroutine(CheckTarget());
    }

    public void Update()
    {
        if(currentTarget != null)
        {
            if(Vector3.Distance(currentTarget.transform.position,this.transform.position) >= attackRange)
            {
                currentTarget = null;
            }
        }
    }

    private IEnumerator CheckTarget()
    {
        while(true)
        {
            //타겟을 찾으면 끝
            int layerMask = 1 << LayerMask.NameToLayer("Monster");
            Collider[] objs = Physics.OverlapSphere(transform.position, attackRange * 0.5f, layerMask);

            foreach(Collider item in objs)
            {
                Monster target = item.GetComponent<Monster>();
                if(target != null)
                {
                    currentTarget = target;
                    break;
                }
            }

            yield return new WaitUntil(CoroutineWait);
            yield return null;
        }
    }

    private bool CoroutineWait()
    {
        return currentTarget == null;
    }
}
