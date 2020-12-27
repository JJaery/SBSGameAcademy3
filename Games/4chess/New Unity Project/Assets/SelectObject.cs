﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    //플레이어가 마우스 클릭을 하면,
    //카메라에서 빛(Ray)을 쏴가지고 부딪치는 게임오브젝트를 판단
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭할 경우 발동
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if(hit.transform != null) // 부딪쳤다.
            {
                Debug.Log(hit.transform.gameObject.name);
                Unit script = hit.transform.GetComponent<Unit>();

                if(script.playerType == GameManager.Instance.currentTurn)
                {
                    script.SelectUnit();
                }
                else if(script.playerType == Unit.PlayerType.Movable)
                {
                    //기물 이동
                    GameManager.Instance.chessBoard.MoveUnit(Unit.SelectedUnit.gameObject, script.curLine, script.curCellNum);
                    Unit.SelectedUnit.isFirstMove = false;
                    Unit.SelectedUnit.DeSelectUnit();
                }
                else
                {
                    Debug.LogError("선택된 플레이어 턴이 아닙니다.");
                    return;
                }
            }
        }
    }
}
