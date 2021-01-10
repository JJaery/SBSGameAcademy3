using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<InventoryManager>();
            return _Instance;
        }
    }
    private static InventoryManager _Instance;


    //플레이어 인벤토리에 필요한 요소들
    //1. 인벤토리 맥스 슬롯
    public static int maxSlot = 54;

    //2.아이템들 정보
    //int -> Slot 번호
    public Dictionary<int, ItemData> itemDataDict = new Dictionary<int, ItemData>();

    //3. 열려있는 인벤토리 팝업 정보
    public UIInventoryPopup curPop;

    public void AddItem(ItemData data)
    {
        AddItem(0, data);
    }

    public void AddItem(int slotNum, ItemData data)
    {
        if (maxSlot < slotNum)
        {
            Debug.LogError("인벤토리가 가득 찼습니다.");
            //Drop
            return;
        }

        if (itemDataDict.ContainsKey(slotNum) == true)
        {
            AddItem(slotNum + 1, data); // 재귀호출 -> 자기 자신을 호출한다. 자기 자신을 호출하는 행위를 하는 메소드(함수)를 재귀 메소드/함수라 합니다.
            return;
        }

        itemDataDict.Add(slotNum, data);
        //if (curPop != null)
        //{
        //    curPop.Refresh();
        //}
        curPop?.Refresh();
    }
}
