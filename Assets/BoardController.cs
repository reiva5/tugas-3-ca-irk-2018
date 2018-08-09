#define BOARD
#define DEBUG
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    private static BoardController instance = null;

    [SerializeField]
    private int boardSize;
    [SerializeField]
    private bool[,] board;
    [SerializeField]
    private int bombs;

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
#endif
        InitData();
        CreateBoard();
        SpreadBombs();
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
    }

    private void CreateBoard()
    {
        BoardView.Instance.CreateBoard(BoardSize);
    }

    private IEnumerator SpreadBombs()
    {
        while (Bombs--)
        {
            break;
        }
    }



    public void CellClick(Transform cell)
    {
        Debug.Log(cell.GetSiblingIndex());
    }
}
