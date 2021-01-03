using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임에 대한 모든 것들을 컨트롤하는 매니저 스크립트
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    private static GameManager _instance = null;

    /// <summary>
    /// 모든 검정 플레이어 유닛들
    /// </summary>
    public static List<Unit> BlackUnits = new List<Unit>();

    /// <summary>
    /// 모든 흰색 플레이어 유닛들
    /// </summary>
    public static List<Unit> WhiteUnits = new List<Unit>();

    #region 프리팹들
    [Header("프리팹들")]
    public GameObject pawnPrefab;
    public GameObject rockPrefab;
    public GameObject knightPrefab;
    public GameObject bishopPrefab;
    public GameObject queenPrefab;
    public GameObject kingPrefab;
    #endregion
    
    public ChessBoard chessBoard;
    public Unit.PlayerType currentTurn;

    private void Start()
    {
        GameStart();
    }

    /// <summary>
    /// 게임을 시작
    /// </summary>
    public void GameStart()
    {
        currentTurn = Unit.PlayerType.White;

        //1.기물 생성
        //2.체스보드에 배치
        Unit script = null;
        //아래 폰 생성
        for (int i = 0; i < 8; i++)
        {
            GameObject pawn = Instantiate(pawnPrefab);
            script = pawn.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(pawn, 1, i);
        }

        //위 폰 생성
        for (int i = 0; i < 8; i++)
        {
            GameObject pawn = Instantiate(pawnPrefab);
            script = pawn.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(pawn, 6, i);
        }

        //나머지 기물들 생성
        {
            GameObject rock1 = Instantiate(rockPrefab);
            script = rock1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(rock1, 0, 0);

            GameObject knight1 = Instantiate(knightPrefab);
            script = knight1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(knight1, 0, 1);

            GameObject bishop1 = Instantiate(bishopPrefab);
            script = bishop1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(bishop1, 0, 2);

            GameObject rock2 = Instantiate(rockPrefab);
            script = rock2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(rock2, 0, 7);

            GameObject knight2 = Instantiate(knightPrefab);
            script = knight2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(knight2, 0, 6);

            GameObject bishop2 = Instantiate(bishopPrefab);
            script = bishop2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(bishop2, 0, 5);

            GameObject queen1 = Instantiate(queenPrefab);
            script = queen1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(queen1, 0, 3);

            GameObject king1 = Instantiate(kingPrefab);
            script = king1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.White);
            chessBoard.MoveUnit(king1, 0, 4);
        }

        {
            GameObject rock1 = Instantiate(rockPrefab);
            script = rock1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(rock1, 7, 0);

            GameObject knight1 = Instantiate(knightPrefab);
            script = knight1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(knight1, 7, 1);

            GameObject bishop1 = Instantiate(bishopPrefab);
            script = bishop1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(bishop1, 7, 2);

            GameObject rock2 = Instantiate(rockPrefab);
            script = rock2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(rock2, 7, 7);

            GameObject knight2 = Instantiate(knightPrefab);
            script = knight2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(knight2, 7, 6);

            GameObject bishop2 = Instantiate(bishopPrefab);
            script = bishop2.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(bishop2, 7, 5);

            GameObject queen1 = Instantiate(queenPrefab);
            script = queen1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(queen1, 7, 3);

            GameObject king1 = Instantiate(kingPrefab);
            script = king1.GetComponent<Unit>();
            script.SetPlayer(Unit.PlayerType.Black);
            chessBoard.MoveUnit(king1, 7, 4);
        }
    }

    public void SwitchTurn()
    {
        if(currentTurn == Unit.PlayerType.White)
        {
            currentTurn = Unit.PlayerType.Black;
        }
        else if( currentTurn == Unit.PlayerType.Black)
        {
            currentTurn = Unit.PlayerType.White;
        }
    }

    /// <summary>
    /// 체크 확인
    /// </summary>
    public void CheckKing()
    {
        
    }
}
