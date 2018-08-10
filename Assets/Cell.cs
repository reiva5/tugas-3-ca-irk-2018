using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private bool clicked = false;

    public void CellClick()
    {
        if (BoardController.Instance.IsWaitingForInput())
        {
            int cellindex = this.transform.GetSiblingIndex();
            BoardController.Instance.CellClick(cellindex);
        }
    }

    public void SetImage(Sprite sprite) {
        GetComponent<Image>().sprite = sprite;
    }

    public bool IsClicked() {
        return clicked == true;
    }

    public void SetClicked() {
        clicked = true;
    }
}
