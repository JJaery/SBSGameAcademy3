using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 아이템의 정보를 그려주는 스크립트
/// </summary>
public class UIInventoryItem : MonoBehaviour
{
    //1.아이콘 정보
    public Image icon;

    //2.아이템 데이터
    public ItemData tableData;

    //3.몇번째 슬롯인지
    public int curSlot;

    public void SetData(int targetSlot,ItemData data)
    {
        if(data == null)
        {
            icon.sprite = null;
        }
        else
        {
            icon.color = Color.red;
        }

        curSlot = targetSlot;
    }
}
