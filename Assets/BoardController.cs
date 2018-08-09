#define BOARD
#define DEBUG
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{

    [SerializeField]
    private Transform cellPrefab;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private int boardSize;
    [SerializeField]
    private bool[,] board;

    // Use this for initialization
    void Start()
    {
#if BOARD
        GameController.Instance = new GameController();
        GameController.Instance.BoardSize = 5;
#endif
        boardSize = GameController.Instance.BoardSize;
        board = new bool[boardSize, boardSize];
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateBoard()
    {
        grid.constraintCount = boardSize;
        for (int i = 0, r, c; i < boardSize * boardSize; i++)
        {
            var cell = Instantiate(cellPrefab, grid.transform);
            r = i / boardSize;
            c = i % boardSize;
            board[r, c] = Random.value < 0.15 ? true : false;
#if DEBUG
            Debug.Log(string.Format("Board[{0}, {1}] = {2}", r, c, board[r, c]));
#endif
        }
    }

    public void CellClick(Transform cell)
    {
        Debug.Log(cell.GetSiblingIndex());
    }
}
