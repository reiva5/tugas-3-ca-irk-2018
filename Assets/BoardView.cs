using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    private static BoardView instance = null;
    private readonly int BombImageIndex = 10;
    private readonly float ShowBoardTimeout = 2.5f;

    [SerializeField]
    private Sprite[] cellSprite;

    [SerializeField]
    private Text notificationText;
    [SerializeField]
    private Text showBoardText;

    [SerializeField]
    private Canvas notificationCanvas;
    [SerializeField]
    private Canvas screenCanvas;
    

    [SerializeField]
    private Transform cellPrefab;

    [SerializeField]
    private GridLayoutGroup grid;
    

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
        if (Instance != null)
        {
            return;
        }
        Instance = this;
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
            if (!BoardController.Instance.IsGameOver())
            {
                BoardController.Instance.GameState = GameState.End;
                ShowAllBomb();
            }
            ShowLoseNotification();
        }
        else
        {
            ShowBombAround(r, c, bombaround);
            if (BoardController.Instance.IsGameWin())
            {
                BoardController.Instance.GameState = GameState.End;
                ShowAllBomb();
                ShowWinNotification();
            }
        }
    }

    private void ShowAllBomb()
    {
        foreach (var index in BoardController.Instance.BombCell)
        {
            int newr = BoardController.Instance.GetRFromIndex(index);
            int newc = BoardController.Instance.GetCFromIndex(index);
            Debug.Log(String.Format("Show image [{0}, {1}]", newr, newc));
            ShowBombImage(newr, newc);
        }
    }

    private void ShowWinNotification()
    {
        string text = "You win!";
        ShowNotification(text);
    }

    private void ShowLoseNotification()
    {
        string text = "You lose!";
        ShowNotification(text);
    }

    private void ShowNotification(string text)
    {
        showBoardText.text = String.Format("Show board ({0}s timeout)", ShowBoardTimeout);
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

    public Cell GetCellAtIndex(int r, int c)
    {
        return Grid.transform.GetChild(r * GameController.Instance.BoardSize + c).gameObject.GetComponent<Cell>();
    }

    public void UpdateCellSize()
    {
        float width = screenCanvas.gameObject.GetComponent<RectTransform>().rect.width;
        float height = screenCanvas.gameObject.GetComponent<RectTransform>().rect.height;
        float val = Math.Min(width, height);

        int size = BoardController.Instance.BoardSize;

        Vector2 newSize = new Vector2(val / size, val / size);
        
        Grid.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

    public IEnumerator ShowBoard()
    {
        notificationCanvas.enabled = false;
        yield return new WaitForSeconds(ShowBoardTimeout);
        notificationCanvas.enabled = true;
    }

    [ContextMenu("Sort Cell Sprites by Name")]
    void SortArray()
    {
        System.Array.Sort(CellSprite, (a, b) => int.Parse(a.name).CompareTo(int.Parse(b.name)));
    }
}
