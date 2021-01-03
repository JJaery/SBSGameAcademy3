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
    public static List<Unit> KillableUnits = new List<Unit>();

    public int curLine;
    public int curCellNum;
    public bool isFirstMove = true;
    public bool isKillable = false;

    public UnitType unitType;
    public Renderer renderer;
    public PlayerType playerType;
    private Color originalColor;

    public void SetKillable()
    {
        isKillable = true;
        originalColor = renderer.materials[1].color;
        renderer.materials[1].color = Color.red;
        KillableUnits.Add(this);
    }

    public void SetPlayer(PlayerType type)
    {
        playerType = type;
        switch(type)
        {
            case PlayerType.White:
                renderer.materials[1].color = Color.white;
                originalColor = renderer.materials[1].color;
                GameManager.WhiteUnits.Add(this);
                break;
            case PlayerType.Black:
                renderer.materials[1].color = Color.black;
                originalColor = renderer.materials[1].color;
                GameManager.BlackUnits.Add(this);
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
        SelectedUnit = this;
    }

    public void DeSelectUnit()
    {
        foreach(Unit script in KillableUnits)
        {
            if (script == null)
                continue;
            script.renderer.materials[1].color = script.originalColor;
            script.isKillable = false;
        }
        KillableUnits.Clear();

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
                    CheckKillableEnemies(curLine + 1, curCellNum + 1);
                    CheckKillableEnemies(curLine + 1, curCellNum - 1);
                    MakeMovableUnit(curLine + 1, curCellNum);
                    if(isFirstMove == true)
                    {
                        MakeMovableUnit(curLine + 2, curCellNum);
                    }
                }
                else if(playerType == PlayerType.Black)
                {
                    CheckKillableEnemies(curLine - 1, curCellNum + 1);
                    CheckKillableEnemies(curLine - 1, curCellNum - 1);
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
            case UnitType.King:
                MakeMovableUnit(curLine + 1, curCellNum + 1);
                MakeMovableUnit(curLine + 1, curCellNum);
                MakeMovableUnit(curLine + 1, curCellNum - 1);

                MakeMovableUnit(curLine, curCellNum + 1);
                MakeMovableUnit(curLine, curCellNum - 1);

                MakeMovableUnit(curLine - 1, curCellNum + 1);
                MakeMovableUnit(curLine - 1, curCellNum);
                MakeMovableUnit(curLine - 1, curCellNum - 1);
                break;
            case UnitType.Rock:
                //윗 방향 직진
                for (int i = curLine + 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i++)
                {
                    if (MakeMovableUnit(i, curCellNum) == false)
                        break;
                }
                //아랫 방향 직진
                for (int i = curLine - 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i--)
                {
                    if(MakeMovableUnit(i, curCellNum) == false)
                        break;
                }
                //오른쪽 방향 직진
                for (int i = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i++)
                {
                    if(MakeMovableUnit(curLine, i) == false)
                        break;
                }
                //왼쪽 방향 직진
                for (int i = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i--)
                {
                    if(MakeMovableUnit(curLine, i) == false)
                        break;
                }
                break;
            case UnitType.Bishop:
                //대각선 오른쪽위 방향
                for (int i = curLine + 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j++)
                {
                    if(MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 왼쪽위 방향
                for (int i = curLine + 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j--)
                {
                    if(MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 오른쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j++)
                {
                    if(MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 왼쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j--)
                {
                    if(MakeMovableUnit(i, j) == false)
                        break;
                }
                break;
            case UnitType.Queen:
                #region 락 + 비숍
                //윗 방향 직진
                for (int i = curLine + 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i++)
                {
                    if (MakeMovableUnit(i, curCellNum) == false)
                        break;
                }
                //아랫 방향 직진
                for (int i = curLine - 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i--)
                {
                    if (MakeMovableUnit(i, curCellNum) == false)
                        break;
                }
                //오른쪽 방향 직진
                for (int i = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i++)
                {
                    if (MakeMovableUnit(curLine, i) == false)
                        break;
                }
                //왼쪽 방향 직진
                for (int i = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i--)
                {
                    if (MakeMovableUnit(curLine, i) == false)
                        break;
                }
                //대각선 오른쪽위 방향
                for (int i = curLine + 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j++)
                {
                    if (MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 왼쪽위 방향
                for (int i = curLine + 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j--)
                {
                    if (MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 오른쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j++)
                {
                    if (MakeMovableUnit(i, j) == false)
                        break;
                }
                //대각선 왼쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j--)
                {
                    if (MakeMovableUnit(i, j) == false)
                        break;
                }
                #endregion
                break;
        }
    }

    private void CheckKillableEnemies(int targetLine, int targetCellNum)
    {
        if (GameManager.Instance.chessBoard.IsInEnemy(targetLine, targetCellNum) == true)
        {
            Unit enemyScript = GameManager.Instance.chessBoard.lines[targetLine].cells[targetCellNum].GetComponentInChildren<Unit>();
            enemyScript.SetKillable();
        }
    }


    private bool MakeMovableUnit(int targetLine,int targetCellNum)
    {
        if(GameManager.Instance.chessBoard.IsInBoard(targetLine,targetCellNum) == false ||
            GameManager.Instance.chessBoard.IsInAlly(targetLine,targetCellNum) == true)
        {
            return false;
        }

        if (GameManager.Instance.chessBoard.IsInEnemy(targetLine, targetCellNum) == true)
        {
            if (unitType != UnitType.Pawn)
            {
                Unit enemyScript = GameManager.Instance.chessBoard.lines[targetLine].cells[targetCellNum].GetComponentInChildren<Unit>();
                enemyScript.SetKillable();
            }
            return false;
        }        

        GameObject movable = Instantiate(gameObject);
        GameManager.Instance.chessBoard.MoveUnit(movable, targetLine, targetCellNum);
        Unit script = movable.GetComponent<Unit>();
        script.SetPlayer(PlayerType.Movable);
        return true;
    }
}
