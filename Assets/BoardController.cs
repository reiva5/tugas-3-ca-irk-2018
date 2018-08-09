#define BOARD
#define DEBUG
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {OpeningCell, WaitingForInput};

public class BoardController : MonoBehaviour
{
    private static BoardController instance = null;

    [SerializeField]
    private int boardSize;
    [SerializeField]
    private bool[,] board;
    [SerializeField]
    private int bombs;

    [SerializeField]
    private GameState gameState = null;

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
        GameController.Instance.Bombs = 10;
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
        Debug.Log(String.Format("Cell [{0}, {1}] clicked!", index / BoardSize, index % BoardSize));
    }

    public bool IsWaitingForInput() {
        return GameState == GameState.WaitingForInput;
    }
}
