using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPopup : MonoBehaviour
{
    public GameObject itemPrefab;

    public GridLayoutGroup grid;

    private void Awake()
    {
        for (int i = 0; i < InventoryManager.maxSlot; i++)
        {
            GameObject obj = Instantiate(itemPrefab, grid.transform);
        }
    }
}
