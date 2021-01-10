﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //플레이어 인벤토리에 필요한 요소들
    //1. 인벤토리 맥스 슬롯
    public static int maxSlot = 50;

    //2.아이템들 정보
    //int -> Slot 번호
    public Dictionary<int, ItemData> itemDataDict = new Dictionary<int, ItemData>();

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
            Debug.LogError("해당 슬롯 번호에 이미 아이템이 존재합니다.");
            AddItem(slotNum + 1, data); // 재귀호출 -> 자기 자신을 호출한다. 자기 자신을 호출하는 행위를 하는 메소드(함수)를 재귀 메소드/함수라 합니다.
            return;
        }

        itemDataDict.Add(slotNum, data);
    }
}
