using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해당 위치에 HPBar를 생성하고 정보를 표시해주는 스크립트
/// </summary>
public class HPBar : MonoBehaviour
{
    public GameObject hpBarPrefab;

    private void Awake()
    {
        GameObject obj = Instantiate(hpBarPrefab);
    }
}
