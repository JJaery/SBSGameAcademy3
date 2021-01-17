using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해당 위치에 HPBar를 생성하고 정보를 표시해주는 스크립트
/// </summary>
public class HPBar : MonoBehaviour
{
    public GameObject hpBarPrefab;

    //어떤 캔버스에 설정할 것인지
    public Transform canvas;

    //어떤 캐릭터의 HP를 보여줄 것인지
    public Character targetScript;
    private GameObject targetObject;
    private RectTransform targetHPBarRect;

    private void Awake()
    {
        targetObject = Instantiate(hpBarPrefab, canvas);
        targetHPBarRect = targetObject.transform.GetChild(0).GetComponent<RectTransform>();
    }


    private void FixedUpdate()
    {
        targetObject.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        targetHPBarRect.sizeDelta = new Vector2(100 * (targetScript.HP / targetScript.MaxHP), targetHPBarRect.sizeDelta.y);
    }
}
