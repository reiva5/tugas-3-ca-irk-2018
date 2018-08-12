using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private bool clicked = false;
    [SerializeField]
    private int bombAround = -1;

    public bool IsClicked
    {
        get
        {
            return clicked;
        }

        set
        {
            clicked = value;
        }
    }

    public int BombAround
    {
        get
        {
            return bombAround;
        }

        set
        {
            bombAround = value;
        }
    }

    public void CellClick()
    {
        if (!IsClicked && BoardController.Instance.IsWaitingForInput())
        {
            SetAsClicked();
            int cellindex = this.transform.GetSiblingIndex();
            BoardController.Instance.CellClick(cellindex);
        }
    }

    public void SetImage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void SetAsClicked()
    {
        IsClicked = true;
    }
}
