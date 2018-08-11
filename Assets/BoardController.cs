// #define BOARD
// #define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Loading, OpeningCell, WaitingForInput };
public enum BoardState { FirstClick, NClick };

public class BoardController : MonoBehaviour
{
    private static readonly int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
    private static readonly int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };
    private static BoardController instance = null;

    [SerializeField]
    private int boardSize;
    [SerializeField]
    private bool[,] board;
    [SerializeField]
    private int bombs;
    [SerializeField]
    private int openedCell = 0;
    [SerializeField]
    private HashSet<int> bombCell = new HashSet<int>();

    [SerializeField]
    private GameState gameState = GameState.Loading;
    [SerializeField]
    private BoardState boardState = BoardState.FirstClick;

    public static BoardController Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public bool[,] Board
    {
        get
        {
            return board;
        }

        set
        {
            board = value;
        }
    }

    public int Bombs
    {
        get
        {
            return bombs;
        }

        set
        {
            bombs = value;
        }
    }

    public int BoardSize
    {
        get
        {
            return boardSize;
        }

        set
        {
            boardSize = value;
        }
    }

    public GameState GameState
    {
        get
        {
            return gameState;
        }

        set
        {
            gameState = value;
        }
    }

    public static int[] Dx
    {
        get
        {
            return dx;
        }
    }

    public static int[] Dy
    {
        get
        {
            return dy;
        }
    }

    public BoardState BoardState
    {
        get
        {
            return boardState;
        }

        set
        {
            boardState = value;
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
#if BOARD
        GameController.Instance = new GameController();
        GameController.Instance.BoardSize = 10;
        GameController.Instance.Bombs = 2;
#endif
        InitData();
        CreateBoard();
        // Debug.Log("Board created!");
        // StartCoroutine(SpreadBombs());
        // Debug.Log("Bombs!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitData()
    {
        BoardSize = GameController.Instance.BoardSize;
        Board = new bool[BoardSize, BoardSize];
        Bombs = GameController.Instance.Bombs;
        GameState = GameState.WaitingForInput;
    }

    private void CreateBoard()
    {
        BoardView.Instance.CreateBoard();
    }

    private IEnumerator SpreadBombs(int r, int c)
    {
        int tbomb = Bombs;
        int i, j;
        for (; tbomb > 0; tbomb--)
        {
            while (true)
            {
                i = GetRandomValue(0, BoardSize);
                j = GetRandomValue(0, BoardSize);
                if (Board[i, j] == false && i != r && j != c) break;
            }
            Board[i, j] = true;
#if DEBUG
            Debug.Log(String.Format("Bomb planted at Board[{0}, {1}]", i, j));
#endif
        }
        yield return null;
    }

    private int GetRandomValue(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void CellClick(int index)
    {
        StartCoroutine(ExploreCell(index));
    }

    public IEnumerator ExploreCell(int index)
    {
        GameState = GameState.OpeningCell;
        int r = index / BoardSize;
        int c = index % BoardSize;

        if (IsFirstClick())
        {
            yield return StartCoroutine(SpreadBombs(r, c));
            BoardState = BoardState.NClick;
        }

        Print("Start Flood Fill!");

        yield return StartCoroutine(FloodFill(r, c));

        Print("End Flood Fill!");

        // if (IsBombCell(r, c))
        // {
        //     Debug.Log(String.Format("Bomb Cell [{0}, {1}] clicked!", r, c));
        // }
        // else
        // {
        //     Debug.Log(String.Format("Cell [{0}, {1}] clicked!", r, c));
        //     Debug.Log(String.Format("Bomb around cell [{0}, {1}]: {2}!", r, c, GetBombAround(r, c)));
        // }
        // ShowCell(r, c);
        GameState = GameState.WaitingForInput;
        yield return null;
    }

    private void Print(string s) {
        Debug.Log(s);
    }

    private void ShowCell(int r, int c)
    {
        if (!IsCellClicked(r, c)) openedCell++;
        BoardView.Instance.ShowCells(r, c, IsBombCell(r, c), GetBombAround(r, c));
    }

    private IEnumerator FloodFill(int r, int c)
    {
        if (GetBombAround(r, c) == 0) {
            Queue q = new Queue();
            Tuple<int, int> t;
            int tr, tc;
            q.Enqueue(new Tuple<int, int>(r, c));
            while (q.Count != 0) {
                t = (Tuple<int, int>) q.Dequeue();
                tr = t.Item1;
                tc = t.Item2;
                ShowCell(tr, tc);
                for (int i = 0, x, y, newr, newc; i < Dx.Length; i++) {
                    x = Dx[i];
                    y = Dy[i];
                    newr = tr+x;
                    newc = tc+y;
                    if (IsCellValid(newr, newc) && !IsCellClicked(newr, newc) && !IsBombCell(newr, newc)) {
                        if (GetBombAround(newr, newc) == 0) {
                            q.Enqueue(Tuple.Create(newr, newc));
                        } else {
                            ShowCell(newr, newc);
                        }
                    } 
                }
            }
        } else {
            ShowCell(r, c);
        }
        yield return null;
    }

    public bool IsGameWin() {
        return openedCell == (BoardSize * BoardSize - Bombs);
    }

    // private void ShowCellAround(r, c) {
    //     if (GetBombAround(r, c) == 0) {
    //         for (int i = 0, x, y, newr, newc; i < Dx.Length; i++) {
    //             x = Dx[i];
    //             y = Dy[i];
    //             newr = r+x;
    //             newc = c+y;
    //             if (IsCellValid(newr, newc) && !IsCellClicked(newr, newc)) {
    //                 ShowCell(newr, newc);
    //             } 
    //         }
    //     }
    // }

    public int GetBombAround(int r, int c)
    {
        int counter = 0;
        for (int i = 0, x, y; i < Dx.Length; i++)
        {
            x = Dx[i];
            y = Dy[i];
            if (IsBombCell(r + x, c + y)) counter++;
        }
        return counter;
    }

    public bool IsCellClicked(int r, int c){
        return BoardView.Instance.GetCellAtIndex(r,c).IsClicked();
    }

    public bool IsCellValid(int r, int c)
    {
        return ((r >= 0) && (r < BoardSize) && (c >= 0) && (c < BoardSize));
    }

    private bool IsBombCell(int r, int c)
    {
        return (IsCellValid(r, c) && (Board[r, c] == true));
    }

    private bool IsNotBombCell(int r, int c)
    {
        return (IsCellValid(r, c) && (Board[r, c] == false));
    }

    public bool IsWaitingForInput()
    {
        return GameState == GameState.WaitingForInput;
    }

    public bool IsFirstClick()
    {
        return BoardState == BoardState.FirstClick;
    }


}
