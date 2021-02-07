using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteHitter : MonoBehaviour
{
    public KeyCode targetKeyCode;
    public Image targetImage;
    public Color pressColor;
    public Color releaseColor;

    private void Awake()
    {
        NoteManager.Instance.hitterList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(targetKeyCode) == true) // 특정키를 눌렀을 때
        {
            OnHit();
            if (SongMakeManager.Instance != null)
                SongMakeManager.Instance.RecordData(targetKeyCode);
        }

        if (Input.GetKey(targetKeyCode) == true) // 특정키를 누르고 있을 때
        {
            targetImage.color = pressColor;
        }
        else
        {
            targetImage.color = releaseColor;
        }
    }

    //특정 키를 눌렀을 때
    void OnHit()
    {
        if (NoteManager.Instance.noteDict.ContainsKey(targetKeyCode) == true
          && NoteManager.Instance.noteDict[targetKeyCode] != null)
        {
            List<Note> targetList = NoteManager.Instance.noteDict[targetKeyCode];

            //히터와 해당 노트의 거리를 가져오면 됩니다.
            //List의 순서는 노트의 생성 순서랑 같습니다.
            //그 이유는 List에 추가되는 조건이 노트가 생성됬을 때 추가되기 때문에
            //즉, 첫번째 요소와 가까울수록 Hitter와 가깝다.
            foreach(Note item in targetList)
            {
                float Dist = item.transform.position.y - this.transform.position.y;
                if(Mathf.Abs(Dist) <= 35) // 35 == NoteHitter의 높이
                {
                    item.OnHit();
                    break;
                }
            }
        }
    }
}
