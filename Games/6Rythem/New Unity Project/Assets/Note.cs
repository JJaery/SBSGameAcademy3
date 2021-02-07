using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    /// <summary>
    /// 빌드할 때 치환되는 상수
    /// const는 string이 아닌 값을 상수로 넣을 때 사용
    /// </summary>
    public const float DEAD_LINE = -400;
    public KeyCode targetKeyCode;

    void Update()
    {
        // 초당 speed 만큼 내려줍니다.
        this.transform.localPosition += Vector3.down * NoteManager.DEFAULT_NOTESPEED * Time.deltaTime;

        if(this.transform.localPosition.y <= DEAD_LINE)
        {
            OnMiss();
        }
    }

    public void OnHit()
    {
        DestroyNote();
    }

    void OnMiss()
    {
        DestroyNote();
    }

    void DestroyNote()
    {
        if (NoteManager.Instance.noteDict.ContainsKey(targetKeyCode) == true
            && NoteManager.Instance.noteDict[targetKeyCode] != null)
        {
            NoteManager.Instance.noteDict[targetKeyCode].Remove(this);
        }
        Destroy(gameObject);
    }
}
