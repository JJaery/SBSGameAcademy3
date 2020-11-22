using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    public Rigidbody _targetRigid;
    public float power;

    void Start()
    {
        Debug.Log("Start가 호출되었습니다.");
    }

    //매 프레임마다 사용자가 해당 키를 눌렀는지 검사한다. (==인풋 검사)
    //물리법칙을 적용할려면 RigidBody를 추가해야한다 == RigidBody가 물리법칙을 담당한다.
    //로켓처럼 쏘고 싶으면 RigidBody의 기능을 이용해야될것같은데?
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            Debug.Log("스페이스가 눌렸습니다.");
            Debug.LogWarning("경고 메세지. - 노란색");
            Debug.LogError("에러 메세지. - 빨간색");
            _targetRigid.AddForce(Vector3.up * power);
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            //transform.position = transform.position + Vector3.left;
            transform.position += Vector3.left;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            //transform.position = transform.position + Vector3.right;
            transform.position += Vector3.right;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.down);
        }
    }
}
