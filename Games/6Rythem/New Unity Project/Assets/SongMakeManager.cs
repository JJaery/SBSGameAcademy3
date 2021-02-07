using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongMakeManager : MonoBehaviour
{
    public static SongMakeManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<SongMakeManager>();
            return _Instance;
        }
    }
    private static SongMakeManager _Instance;


    public UnityEngine.UI.Text countingText;
    public VideoPlayer targetPlayer;
    public SongData songData;

    public void OnClickLoadVideoButton()
    {
        string filePath = UnityEditor.EditorUtility.OpenFilePanel("실행시킬 뮤비를 선택하세요", "", "mp4");
        filePath = filePath
            .Replace($"{Application.dataPath}/Resources/", "")
            .Replace(".mp4","");
        Debug.Log(filePath);
        VideoClip clip = Resources.Load<VideoClip>(filePath);
        targetPlayer.clip = clip;
        targetPlayer.Play();
        songData = new SongData();
        songData.videoPath = filePath;
    }

    public void OnClickSaveButton()
    {
        string filePath = UnityEditor.EditorUtility.SaveFilePanel("저장할 위치를 지정해주세요", "", "", "json.txt");
        System.IO.File.WriteAllText(filePath, JsonUtility.ToJson(songData));
    }

    public void RecordData(KeyCode keyCode)
    {
        if (targetPlayer.isPlaying == false)
            return;

        songData.datas.Add(new NoteMakeData()
        {
            time = targetPlayer.clockTime,
            keyCode = keyCode
        });

        countingText.text = $"Note Count : {songData.datas.Count}";
    }
}

public class SongData // 노트 생성 데이터
{
    public string title; // 노래 제목
    public string singer; // 가수
    public string videoPath; // Resources에 있는 Video 경로
    [SerializeField]
    public List<NoteMakeData> datas = new List<NoteMakeData>(); //노트 생성 데이터
}

[System.Serializable]
public class NoteMakeData
{
    public double time; // 노트가 생성될 시점(VideoPlayer의 ClockTime)
    public KeyCode keyCode; // 노트가 배정 될 키코드
}
