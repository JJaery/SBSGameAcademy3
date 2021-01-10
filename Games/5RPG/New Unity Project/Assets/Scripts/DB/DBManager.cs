using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DBManager : MonoBehaviour
{
    public TextAsset dbFile;
    /// <summary>
    /// key(int) -> 아이템 ID
    /// </summary>
    public static Dictionary<int, ItemData> ItemDB;

    /// <summary>
    /// TextAsset을 Resources.Load로 가져오려면 확장자가 .txt 파일 이어야 됩니다.
    /// </summary>
    private void Start()
    {
        Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dbFile.text);
        ItemDB = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(data["ItemDB"].ToString());
    }
}
