using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
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
}
