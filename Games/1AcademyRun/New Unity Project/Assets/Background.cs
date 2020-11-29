using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    ///백그라운드 클래스가 가져야 될 기능들
    /// 1. 뒤로 이동되는 기능 (필요 데이터 : 이동되는 속도)  
    /// 끗

    public float speed;

    //매프레임마다 호출
    private void Update()
    {
        //transform.position += new Vector3(-1, 0, 0);
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
