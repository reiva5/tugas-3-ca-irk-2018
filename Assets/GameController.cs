using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static GameController instance = null;

    [SerializeField]
    private int boardSize = -1;

    [SerializeField]
    private int bombs = -1;

    public static GameController Instance
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBoardSize(Text boardsize)
    {
        BoardSize = int.Parse(boardsize.text);
        BoardSize = Mathf.Clamp(BoardSize, 1, 100);
        EnterGame();
    }

    public void SetBombs(Text bombs)
    {
        Bombs = int.Parse(bombs.text);
        Bombs = Mathf.Clamp(Bombs, 1, BoardSize * BoardSize - 1);
        EnterGame();
    }

    public void EnterGame()
    {
        if (IsBoardSizeValid() && IsBombsValid())
        {
            EnterBoard();
        }
    }

    public bool IsBoardSizeValid()
    {
        var BoardMin = 1;
        var BoardMax = 20;
        return BoardMin <= BoardSize && BoardSize <= BoardMax;
    }

    public bool IsBombsValid()
    {
        var BombMin = 1;
        var BombMax = (BoardSize * BoardSize) / 3;
        return BombMin <= Bombs && Bombs <= BombMax;
    }

    public void ResetData()
    {
        BoardSize = -1;
        Bombs = -1;
    }

    public void BackToMainMenu()
    {
        SceneLoader.LoadScene(0);
    }

    public void EnterBoard()
    {
        SceneLoader.LoadScene(1);
    }
}
