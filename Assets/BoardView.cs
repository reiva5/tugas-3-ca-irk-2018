using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    private static BoardView instance = null;
    private readonly int BombImageIndex = 10;
    [SerializeField]
    private Text notificationText;
    [SerializeField]
    private Canvas notificationCanvas;
    [SerializeField]
    private Sprite[] cellSprite;

    [SerializeField]
    private Transform cellPrefab;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private Canvas canvas;

    public static BoardView Instance
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

    public GridLayoutGroup Grid
    {
        get
        {
            return grid;
        }

        set
        {
            grid = value;
        }
    }

    public Sprite[] CellSprite
    {
        get
        {
            return cellSprite;
        }

        set
        {
            cellSprite = value;
        }
    }

    void Awake()
    {
        if (Instance != null) {
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

    public void CreateBoard()
    {
        int boardSize = BoardController.Instance.BoardSize;
        Grid.constraintCount = boardSize;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            Instantiate(cellPrefab, Grid.transform);
        }
        UpdateCellSize();
    }

    public void ShowCells(int r, int c, bool bombcell, int bombaround)
    {
        if (bombcell)
        {
            ShowBombImage(r, c);
            // if ()
            // BoardController.Instance.ShowAllBoard();
            ShowLoseNotification();
        }
        else
        {
            ShowBombAround(r, c, bombaround);
        }
        SetClicked(r, c);

        if (BoardController.Instance.IsGameWin()) {
            ShowWinNotification();
            // Debug.Log("Win?");
        }
    }

    private void ShowWinNotification() {
        string text = "You win!";
        ShowNotification(text);
    }

    private void ShowLoseNotification()
    {
        string text = "You lose!";
        ShowNotification(text);
    }

    private void ShowNotification(string text) {
        notificationText.text = text;
        notificationCanvas.enabled = true;
    }

    private void ShowBombAround(int r, int c, int bombaround)
    {
        Cell cell = GetCellAtIndex(r, c);
        cell.SetImage(CellSprite[bombaround]);
    }

    private void ShowBombImage(int r, int c)
    {
        Cell cell = GetCellAtIndex(r, c);
        cell.SetImage(CellSprite[BombImageIndex]);
    }

    public void BackToMainMenu() {
        GameController.Instance.BackToMainMenu();
    }

    public Cell GetCellAtIndex(int r, int c)
    {
        return Grid.transform.GetChild(r * GameController.Instance.BoardSize + c).gameObject.GetComponent<Cell>();
    }

    [ContextMenu("Sort Cell Sprites by Name")]
    void SortArray()
    {
        System.Array.Sort(CellSprite, (a, b) => int.Parse(a.name).CompareTo(int.Parse(b.name)));
    }

    public void SetClicked(int r, int c) {
        GetCellAtIndex(r, c).SetClicked();
    }

    public void UpdateCellSize(){
        float width = canvas.gameObject.GetComponent<RectTransform>().rect.width;
        float height = canvas.gameObject.GetComponent<RectTransform>().rect.height;
        float val = Math.Min(width, height);
        Debug.Log(height + " " + width);
        int size = BoardController.Instance.BoardSize;
        Vector2 newSize = new Vector2(val / size, val / size);
        Debug.Log(newSize);
        Grid.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
