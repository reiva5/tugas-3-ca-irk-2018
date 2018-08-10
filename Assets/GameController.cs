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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
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
    }

    public void SetBombs(Text bombs)
    {
        Bombs = int.Parse(bombs.text);
        Bombs = Mathf.Clamp(Bombs, 1, 100);
    }

    public void EnterGame()
    {
        if (BoardSize > 0 && Bombs > 0)
        {
            SceneLoader.LoadScene(1);
        }
    }
}
