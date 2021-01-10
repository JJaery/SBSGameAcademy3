using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPopup : MonoBehaviour
{
    public GameObject itemPrefab;

    public GridLayoutGroup grid;

    private Dictionary<int, UIInventoryItem> items = new Dictionary<int, UIInventoryItem>();


    private void Awake()
    {
        InventoryManager.Instance.curPop = this;

        for (int i = 0; i < InventoryManager.maxSlot; i++)
        {
            GameObject obj = Instantiate(itemPrefab, grid.transform);
            UIInventoryItem script = obj.GetComponent<UIInventoryItem>();
            
            if(InventoryManager.Instance.itemDataDict.ContainsKey(i) == true)
            {
                script.SetData(i, InventoryManager.Instance.itemDataDict[i]);
            }
            else
            {
                script.SetData(i, null); //빈칸
            }

            items[i] = script;
        }
        //테스트코드
        StartCoroutine(TestCode());
    }

    //인벤토리 정보가 변경될 때 호출됩니다.
    public void Refresh()
    {
        foreach(var kv in items)
        {
            if (InventoryManager.Instance.itemDataDict.ContainsKey(kv.Key) == true)
            {
                kv.Value.SetData(kv.Key, InventoryManager.Instance.itemDataDict[kv.Key]);
            }
            else
            {
                kv.Value.SetData(kv.Key, null); //빈칸
            }
        }
    }

    IEnumerator TestCode()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            InventoryManager.Instance.AddItem(DBManager.ItemDB[1]);
        }
    }
}
