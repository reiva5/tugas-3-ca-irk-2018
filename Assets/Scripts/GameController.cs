using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;

    public static readonly int MinBoard = 1;
    public static readonly int MaxBoard = 20;
    public static readonly int MinBomb = 0;

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

    void Start()
    {
        InitText();
    }

    private void InitText()
    {
        GameView.Instance.SetBoardText();
        GameView.Instance.SetBombText();
    }

    public void SetBoardSize(Text boardsize)
    {
        BoardSize = int.Parse(boardsize.text);
        InitText();
    }

    public void SetBombs(Text bombs)
    {
        Bombs = int.Parse(bombs.text);
        InitText();
    }

    public void EnterGame()
    {
        if (IsBoardSizeValid() && IsBombsValid())
        {
            EnterBoard();
        }
        else
        {
            StartCoroutine(GameView.Instance.AutoHideInvalidInputCanvas());
        }
    }

    public bool IsBoardSizeValid()
    {
        return MinBoard <= BoardSize && BoardSize <= MaxBoard;
    }

    public bool IsBombsValid()
    {
        return MinBomb <= Bombs && Bombs <= GetMaxBombs();
    }

    public void BackToMainMenu()
    {
        SceneLoader.LoadScene(0);
    }

    public void EnterBoard()
    {
        SceneLoader.LoadScene(1);
    }

    public int GetMaxBombs()
    {
        return (int)Mathf.Pow(BoardSize, 2) / 3;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
