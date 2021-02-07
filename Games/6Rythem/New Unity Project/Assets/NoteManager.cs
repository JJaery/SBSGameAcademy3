using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<NoteManager>();
            }
            if (_Instance == null) // Find했는데도 없다?
            {
                GameObject obj = new GameObject("Manager"); // Manager 오브젝트 만들고
                _Instance = obj.AddComponent<NoteManager>(); // Manager에다가 스크립트 부착
            }
            return _Instance;
        }
    }
    private static NoteManager _Instance;

    public static readonly float DEFAULT_NOTESPEED = 1600;
    public static readonly float NOTE_SPAWN_HEIGHT = 800;
    public static readonly float START_DELAY_TIME = NOTE_SPAWN_HEIGHT / DEFAULT_NOTESPEED;
    


    //NoteHitter
    public List<NoteHitter> hitterList; // <- 우리가 만든 NoteHitter들
    public GameObject notePrefab;
    public Canvas parentCanvas;
    public VideoPlayer targetPlayer;


    
    //생성되어 있는 모든 노트들의 정보 Dictionary
    public Dictionary<KeyCode, List<Note>> noteDict 
        = new Dictionary<KeyCode, List<Note>>();

    private SongData curData;

    public void LoadGameData()
    {
        string filePath = UnityEditor.EditorUtility.OpenFilePanel("불러올 음악을 지정하세요.", "", "json.txt");
        string jsonStr = System.IO.File.ReadAllText(filePath);
        curData = JsonUtility.FromJson<SongData>(jsonStr);
        VideoClip clip = Resources.Load<VideoClip>(curData.videoPath);
        targetPlayer.clip = clip;
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        //3초 내 노트들을 판단하여 노트들을 생성하면 됩니다.
        //Sync 맞추는 법.
        //NoteHitter 에서 생성 지점까지의 거리, 속력을 알면 Note가 몇초뒤에 Hitter에 도착하는지 알 수 있다.
        //시간 = 거리 / 속력
        //시간 = 800 / 100
        //노트가 생성되고 Hitter에 도달해야되는 시간은 8초 == 8초 미리 생성되어야 한다.

        NoteMakeData curMakeData = curData.datas[0];
        while (curMakeData.time <= START_DELAY_TIME)
        {
            CreateNote(curMakeData.keyCode, (float)curMakeData.time * DEFAULT_NOTESPEED);
            curData.datas.RemoveAt(0);

            if (curData.datas.Count > 0)
            {
                curMakeData = curData.datas[0];
            }
        }

        yield return new WaitForSeconds(START_DELAY_TIME);
        targetPlayer.Play();
    }


    private void Update()
    {
        if(curData != null)
        {
            if (curData.datas.Count > 0)
            {
                NoteMakeData curMakeData = curData.datas[0];
                if (curMakeData.time <= targetPlayer.clockTime + START_DELAY_TIME)
                {
                    CreateNote(curMakeData.keyCode);
                    curData.datas.RemoveAt(0);
                }
            }
        }
    }


    private void CreateNote(KeyCode keyCode, float offset = 0f)
    {
        NoteHitter targetHitter = null;
        //해당 키코드에 맞는 Hitter를 찾는게 우선.
        foreach (NoteHitter item in hitterList)
        {
            if (item.targetKeyCode == keyCode)
            {
                targetHitter = item;
                break;
            }
        }

        if(targetHitter == null) //찾지 못했다.
        {
            Debug.LogError($"해당 키코드에 맞는 히터를 찾을 수 없습니다. KeyCode : {keyCode}");
            return;
        }

        //찾기 성공. Note프리팹을 (Hitter의 Y값 위치 + a) 위치에 생성
        GameObject obj = Instantiate(notePrefab, parentCanvas.transform);
        obj.transform.position = targetHitter.transform.position + Vector3.up * (NOTE_SPAWN_HEIGHT + offset);
        Note script = obj.GetComponent<Note>();
        script.targetKeyCode = keyCode;

        if(noteDict.ContainsKey(keyCode) == false)
        {
            noteDict[keyCode] = new List<Note>();
        }

        noteDict[keyCode].Add(script);
    }
}
