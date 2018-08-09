#define BOARD
#define DEBUG
using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    private static BoardController instance = null;

    [SerializeField]
    private int boardSize;
    [SerializeField]
    private bool[,] board;

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
        boardSize = GameController.Instance.BoardSize;
        Board = new bool[boardSize, boardSize];
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateBoard()
    {
        BoardView.Instance.CreateBoard(boardSize);
    }

    private void SpreadBombs()
    {

    }

    public void CellClick(Transform cell)
    {
        Debug.Log(cell.GetSiblingIndex());
    }
}
