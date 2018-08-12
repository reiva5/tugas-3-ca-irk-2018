// #define BOARD
// #define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Loading, OpeningCell, WaitingForInput, Lost };
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

    public HashSet<int> BombCell
    {
        get
        {
            return bombCell;
        }

        set
        {
            bombCell = value;
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


    void Start()
    {
#if BOARD
        GameController.Instance = new GameController();
        GameController.Instance.BoardSize = 12;
        GameController.Instance.Bombs = 2;
#endif
        ResetData();
        CreateBoard();
    }


    void Update()
    {

    }

    public void ResetData()
    {
        BoardSize = GameController.Instance.BoardSize;
        Board = new bool[BoardSize, BoardSize];
        Bombs = GameController.Instance.Bombs;
        GameState = GameState.WaitingForInput;
    }

    private void UpdateCellSize()
    {
        BoardView.Instance.UpdateCellSize();
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
                if (i != r && j != c && Board[i, j] == false) break;
            }
            Board[i, j] = true;
            BombCell.Add(i * BoardSize + j);
#if DEBUG
            Debug.Log(String.Format("Bomb planted at Board[{0}, {1}]", i, j));
            Debug.Log(String.Format("Bomb planted at index {0}", i * BoardSize + j));
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
        int r = GetRFromIndex(index);
        int c = GetCFromIndex(index);

        if (IsFirstClick())
        {
            yield return StartCoroutine(SpreadBombs(r, c));
            BoardState = BoardState.NClick;
        }

        Print("Start Flood Fill!");

        if (!IsCellClicked(r, c))
        {
            yield return StartCoroutine(FloodFill(r, c));
        }

        Print("End Flood Fill!");











        GameState = GameState.WaitingForInput;
        yield return null;
    }

    private void Print(string s)
    {
        Debug.Log(s);
    }

    private void ShowCell(int r, int c)
    {
        if (!IsCellClicked(r, c)) {
            bool isBomb = IsBombCell(r, c);
            if (!isBomb) openedCell++;
            BoardView.Instance.GetCellAtIndex(r, c).SetAsClicked();
            BoardView.Instance.ShowCells(r, c, isBomb, GetBombAround(r, c));
        } 
    }

    private IEnumerator FloodFill(int r, int c)
    {
        if (!IsBombCell(r, c) && GetBombAround(r, c) == 0)
        {
            Queue q = new Queue();
            KeyValuePair<int, int> t;
            int tr, tc;
            q.Enqueue(CreatePair(r, c));
            while (q.Count != 0)
            {
                t = (KeyValuePair<int, int>)q.Dequeue();
                tr = t.Key;
                tc = t.Value;
                ShowCell(tr, tc);
                for (int i = 0, x, y, newr, newc; i < Dx.Length; i++)
                {
                    x = Dx[i];
                    y = Dy[i];
                    newr = tr + x;
                    newc = tc + y;
                    if (IsCellValid(newr, newc) && !IsCellClicked(newr, newc) && !IsBombCell(newr, newc))
                    {
                        if (GetBombAround(newr, newc) == 0)
                        {
                            ShowCell(newr, newc);
                            q.Enqueue(CreatePair(newr, newc));
                        }
                        else
                        {
                            ShowCell(newr, newc);
                        }
                    }
                }
            }
        }
        else
        {
            ShowCell(r, c);
        }
        yield return null;
    }

    private KeyValuePair<int, int> CreatePair(int r, int c)
    {
        return new KeyValuePair<int, int>(r, c);
    }

    public bool IsGameWin()
    {
        return !IsAlreadyLost() && openedCell == (BoardSize * BoardSize - Bombs);
    }

    public int GetBombAround(int r, int c)
    {
        Cell cell = BoardView.Instance.GetCellAtIndex(r, c);
        if (cell.BombAround == -1)
        {
            int counter = 0;
            for (int i = 0, x, y; i < Dx.Length; i++)
            {
                x = Dx[i];
                y = Dy[i];
                if (IsBombCell(r + x, c + y)) counter++;
            }
            cell.BombAround = counter;
        }
        return cell.BombAround;
    }

    public bool IsCellClicked(int r, int c)
    {
        return BoardView.Instance.GetCellAtIndex(r, c).IsClicked;
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

    public bool IsAlreadyLost()
    {
        return GameState == GameState.Lost;
    }

    public int GetIndexFromRC(int r, int c)
    {
        return r * BoardSize + c;
    }

    public int GetRFromIndex(int index)
    {
        return index / BoardSize;
    }

    public int GetCFromIndex(int index)
    {
        return index % BoardSize;
    }
}
