#define BOARD
#define DEBUG
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Loading, OpeningCell, WaitingForInput };

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
    private GameState gameState = GameState.Loading;

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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
#if BOARD
        GameController.Instance = new GameController();
        GameController.Instance.BoardSize = 5;
        GameController.Instance.Bombs = 7;
#endif
        InitData();
        CreateBoard();
        Debug.Log("Board created!");
        StartCoroutine(SpreadBombs());
        Debug.Log("Bombs!");
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
        BoardView.Instance.CreateBoard(BoardSize);
    }

    private IEnumerator SpreadBombs()
    {
        int i, j;
        for (; Bombs > 0; Bombs--)
        {
            while (true)
            {
                i = GetRandomValue(0, BoardSize);
                j = GetRandomValue(0, BoardSize);
                if (Board[i, j] == false) break;
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
        GameState = GameState.OpeningCell;
        int r = index / BoardSize;
        int c = index % BoardSize;
        if (IsBombCell(r, c))
        {
            // BoardView.Instance.ShowAllCells();
            // GameView.Instance.ShowLoseNotification();
            Debug.Log(String.Format("Bomb Cell [{0}, {1}] clicked!", r, c));
        }
        else
        {
            Debug.Log(String.Format("Cell [{0}, {1}] clicked!", r, c));
            Debug.Log(String.Format("Bomb around cell [{0}, {1}]: {2}!", r, c, GetBombAround(r, c)));
        }
        BoardView.Instance.ShowCells(r, c, IsBombCell(r, c), GetBombAround(r,c ));
        GameState = GameState.WaitingForInput;
    }

    private void FloodFill(int r, int c)
    {

    }

    public int GetBombAround(int r, int c)
    {
        int counter = 0;
        for (int i = 0, x, y; i < Dx.Length; i++)
        {
            x = Dx[i];
            y = Dy[i];
            if (IsBombCell(r+x, c+y)) counter++;
        }
        return counter;
    }

    public bool IsCellValid(int r, int c)
    {
        return ((r >= 0) && (r < BoardSize) && (c >= 0) && (c < BoardSize));
    }

    private bool IsBombCell(int r, int c)
    {
        return (IsCellValid(r, c) && (Board[r, c] == true));
    }

    private bool IsNotBombCell(int r, int c) {
        return (IsCellValid(r, c) && (Board[r, c] == false));
    }

    public bool IsWaitingForInput()
    {
        return GameState == GameState.WaitingForInput;
    }
}
