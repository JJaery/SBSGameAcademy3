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
            DestroyImmediate(script.gameObject);
        }
        MovabledUnits.Clear();
    }

    public void MakeMovableUnits(List<Unit> killableUnitList = null)
    {
        switch(unitType)
        {
            case UnitType.Pawn:
                if(playerType == PlayerType.White)
                {
                    CheckKillableEnemies(curLine + 1, curCellNum + 1, killableUnitList);
                    CheckKillableEnemies(curLine + 1, curCellNum - 1, killableUnitList);
                    MakeMovableUnit(curLine + 1, curCellNum, killableUnitList);
                    if(isFirstMove == true)
                    {
                        MakeMovableUnit(curLine + 2, curCellNum, killableUnitList);
                    }
                }
                else if(playerType == PlayerType.Black)
                {
                    CheckKillableEnemies(curLine - 1, curCellNum + 1, killableUnitList);
                    CheckKillableEnemies(curLine - 1, curCellNum - 1, killableUnitList);
                    MakeMovableUnit(curLine - 1, curCellNum, killableUnitList);
                    if (isFirstMove == true)
                    {
                        MakeMovableUnit(curLine - 2, curCellNum, killableUnitList);
                    }
                }
                break;
            case UnitType.Knight:
                {
                    MakeMovableUnit(curLine + 2, curCellNum - 1, killableUnitList);
                    MakeMovableUnit(curLine + 2, curCellNum + 1, killableUnitList);

                    MakeMovableUnit(curLine - 2, curCellNum - 1, killableUnitList);
                    MakeMovableUnit(curLine - 2, curCellNum + 1, killableUnitList);

                    MakeMovableUnit(curLine + 1, curCellNum - 2, killableUnitList);
                    MakeMovableUnit(curLine - 1, curCellNum - 2, killableUnitList);

                    MakeMovableUnit(curLine + 1, curCellNum + 2, killableUnitList);
                    MakeMovableUnit(curLine - 1, curCellNum + 2, killableUnitList);
                    break;
                }
            case UnitType.King:
                {
                    MakeMovableUnit(curLine + 1, curCellNum + 1, killableUnitList);
                    MakeMovableUnit(curLine + 1, curCellNum, killableUnitList);
                    MakeMovableUnit(curLine + 1, curCellNum - 1, killableUnitList);

                    MakeMovableUnit(curLine, curCellNum + 1, killableUnitList);
                    MakeMovableUnit(curLine, curCellNum - 1, killableUnitList);

                    MakeMovableUnit(curLine - 1, curCellNum + 1, killableUnitList);
                    MakeMovableUnit(curLine - 1, curCellNum, killableUnitList);
                    MakeMovableUnit(curLine - 1, curCellNum - 1, killableUnitList);
                    break;
                }
            case UnitType.Rock:
                //윗 방향 직진
                for (int i = curLine + 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i++)
                {
                    if (MakeMovableUnit(i, curCellNum, killableUnitList) == false)
                        break;
                }
                //아랫 방향 직진
                for (int i = curLine - 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i--)
                {
                    if(MakeMovableUnit(i, curCellNum, killableUnitList) == false)
                        break;
                }
                //오른쪽 방향 직진
                for (int i = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i++)
                {
                    if(MakeMovableUnit(curLine, i, killableUnitList) == false)
                        break;
                }
                //왼쪽 방향 직진
                for (int i = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i--)
                {
                    if(MakeMovableUnit(curLine, i, killableUnitList) == false)
                        break;
                }
                break;
            case UnitType.Bishop:
                //대각선 오른쪽위 방향
                for (int i = curLine + 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j++)
                {
                    if(MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 왼쪽위 방향
                for (int i = curLine + 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j--)
                {
                    if(MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 오른쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j++)
                {
                    if(MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 왼쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j--)
                {
                    if(MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                break;
            case UnitType.Queen:
                #region 락 + 비숍
                //윗 방향 직진
                for (int i = curLine + 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i++)
                {
                    if (MakeMovableUnit(i, curCellNum, killableUnitList) == false)
                        break;
                }
                //아랫 방향 직진
                for (int i = curLine - 1; GameManager.Instance.chessBoard.IsInBoard(i, curCellNum); i--)
                {
                    if (MakeMovableUnit(i, curCellNum, killableUnitList) == false)
                        break;
                }
                //오른쪽 방향 직진
                for (int i = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i++)
                {
                    if (MakeMovableUnit(curLine, i, killableUnitList) == false)
                        break;
                }
                //왼쪽 방향 직진
                for (int i = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(curLine, i); i--)
                {
                    if (MakeMovableUnit(curLine, i, killableUnitList) == false)
                        break;
                }
                //대각선 오른쪽위 방향
                for (int i = curLine + 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j++)
                {
                    if (MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 왼쪽위 방향
                for (int i = curLine + 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i++, j--)
                {
                    if (MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 오른쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum + 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j++)
                {
                    if (MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                //대각선 왼쪽 아래 방향
                for (int i = curLine - 1, j = curCellNum - 1; GameManager.Instance.chessBoard.IsInBoard(i, j); i--, j--)
                {
                    if (MakeMovableUnit(i, j, killableUnitList) == false)
                        break;
                }
                #endregion
                break;
        }
    }

    private void CheckKillableEnemies(int targetLine, int targetCellNum, List<Unit> killableUnitList)
    {
        if (GameManager.Instance.chessBoard.IsInEnemy(targetLine, targetCellNum) == true)
        {
            Unit enemyScript = GameManager.Instance.chessBoard.lines[targetLine].cells[targetCellNum].GetComponentInChildren<Unit>();
            if (killableUnitList != null)
            {
                killableUnitList.Add(enemyScript);
            }
            else
            {
                if (GameManager.Instance.isCheck == true || this.unitType == UnitType.King)
                {
                    if (GameManager.Instance.SimulateCheck(this, targetLine, targetCellNum) == false)//체크회피성공
                    {
                        enemyScript.SetKillable();
                    }
                }
                else
                {
                    enemyScript.SetKillable();
                }
            }
        }
    }

    private bool MakeMovableUnit(int targetLine,int targetCellNum, List<Unit> killableUnitList)
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
                CheckKillableEnemies(targetLine, targetCellNum, killableUnitList);
            }
            return false;
        }

        if (killableUnitList == null)
        {
            if (GameManager.Instance.isCheck == true || this.unitType == UnitType.King)
            {
                //체크일 때 처리
                //targetLine하고 targetCellNum으로 이동할 경우 체크가 회피 되는지 검사
                if (GameManager.Instance.SimulateCheck(this, targetLine, targetCellNum) == false)//체크회피성공
                {
                    GameObject movable = Instantiate(gameObject);
                    GameManager.Instance.chessBoard.MoveUnit(movable, targetLine, targetCellNum);
                    Unit script = movable.GetComponent<Unit>();
                    script.SetPlayer(PlayerType.Movable);
                }
            }
            else
            {
                GameObject movable = Instantiate(gameObject);
                GameManager.Instance.chessBoard.MoveUnit(movable, targetLine, targetCellNum);
                Unit script = movable.GetComponent<Unit>();
                script.SetPlayer(PlayerType.Movable);
            }
        }
        return true;
    }

    public void IsCheck(ref bool result)
    {
        if(result == true) // 체크된 상태
        {
            return;
        }

        List<Unit> killableList = new List<Unit>();
        MakeMovableUnits(killableList);
        if (killableList.Count != 0)
        {
            //Debug.Log($"Player : {playerType} / UnitType : {unitType}[{curLine},{curCellNum}] / killableCount - {killableList.Count}");
            foreach (Unit killableUnit in killableList)
            {
                //Debug.Log($"    {killableUnit.unitType} [{killableUnit.curLine},{killableUnit.curCellNum}]");
                //result - false    unitType == King. => true
                // |= <- 이 표현은 bool = bool | bool
                // &= <- 이 표현은 bool = bool & bool
                result |= killableUnit.unitType == UnitType.King;

                if(result == true) // 킹이 체크 된 상태
                {
                    break;
                }
            }
        }
    }
}
