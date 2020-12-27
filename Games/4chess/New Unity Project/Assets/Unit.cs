using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitType
    {
        Pawn,
        Knight,
        Rock,
        Bishop,
        Queen,
        King
    }
    public enum PlayerType
    {
        None,
        White,
        Black,
        Movable
    }
    public static Unit SelectedUnit = null;
    public static List<Unit> MovabledUnits = new List<Unit>();

    public int curLine;
    public int curCellNum;
    public bool isFirstMove = true;

    public UnitType unitType;
    public Renderer renderer;
    public PlayerType playerType;
    private Color originalColor;

    public void SetPlayer(PlayerType type)
    {
        playerType = type;
        switch(type)
        {
            case PlayerType.White:
                renderer.materials[1].color = Color.white;
                break;
            case PlayerType.Black:
                renderer.materials[1].color = Color.black;
                break;
            case PlayerType.Movable:
                renderer.materials[1].color = Color.green;
                MovabledUnits.Add(this);
                break;
            default:
                Debug.LogError("알맞지 않은 플레이어 타입입니다.");
                break;
        }
    }

    public void SelectUnit()
    {
        if (SelectedUnit == this)
            return;

        if(SelectedUnit != null)
        {
            SelectedUnit.DeSelectUnit();
        }
        MakeMovableUnits();
        //선택될 때 행동
        originalColor = renderer.materials[1].color;
        renderer.materials[1].color = Color.red;
        SelectedUnit = this;
    }

    public void DeSelectUnit()
    {
        //선택해제될 때 행동
        renderer.materials[1].color = originalColor;
        DestroyMovableUnits();
        SelectedUnit = null;
    }

    public void DestroyMovableUnits()
    {
        foreach (Unit script in MovabledUnits)
        {
            Destroy(script.gameObject);
        }
        MovabledUnits.Clear();
    }

    public void MakeMovableUnits()
    {
        switch(unitType)
        {
            case UnitType.Pawn:
                if(playerType == PlayerType.White)
                {
                    MakeMovableUnit(curLine + 1, curCellNum);
                    if(isFirstMove == true)
                    {
                        MakeMovableUnit(curLine + 2, curCellNum);
                    }
                }
                else if(playerType == PlayerType.Black)
                {
                    MakeMovableUnit(curLine - 1, curCellNum);
                    if (isFirstMove == true)
                    {
                        MakeMovableUnit(curLine - 2, curCellNum);
                    }
                }
                break;
            case UnitType.Knight:
                MakeMovableUnit(curLine + 2, curCellNum - 1);
                MakeMovableUnit(curLine + 2, curCellNum + 1);

                MakeMovableUnit(curLine - 2, curCellNum - 1);
                MakeMovableUnit(curLine - 2, curCellNum + 1);

                MakeMovableUnit(curLine + 1, curCellNum - 2);
                MakeMovableUnit(curLine - 1, curCellNum - 2);

                MakeMovableUnit(curLine + 1, curCellNum + 2);
                MakeMovableUnit(curLine - 1, curCellNum + 2);
                break;
        }
    }

    private void MakeMovableUnit(int targetLine,int targetCellNum)
    {
        GameObject movable = Instantiate(gameObject);
        GameManager.Instance.chessBoard.MoveUnit(movable, targetLine, targetCellNum);
        Unit script = movable.GetComponent<Unit>();
        script.SetPlayer(PlayerType.Movable);
    }
}
