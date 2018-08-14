using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private static GameView instance = null;
    [SerializeField]
    private Text boardText;
    [SerializeField]
    private Text bombText;
    [SerializeField]
    private Canvas invalidInputCanvas;

    public static GameView Instance
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

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    public void SetBoardText()
    {
        string text = String.Format("Enter board size: \n(range [{0}..{1}] inclusive)",
            GameController.MinBoard.ToString(),
            GameController.MaxBoard.ToString());
        boardText.text = text;
    }

    public void SetBombText()
    {
        string text = String.Format("Enter number of bombs: \n(range [{0}..{1}] inclusive)",
            GameController.MinBomb.ToString(),
            GameController.Instance.IsBoardSizeValid() ?
                GameController.Instance.GetMaxBombs().ToString() :
                "1/3 total cells");
        bombText.text = text;
    }

    private void ShowInvalidInputNotification()
    {
        invalidInputCanvas.enabled = true;
    }

    private void HideInvalidInputNotification()
    {
        invalidInputCanvas.enabled = false;
    }

    public IEnumerator AutoHideInvalidInputCanvas()
    {
        ShowInvalidInputNotification();
        yield return new WaitForSeconds(1f);
        HideInvalidInputNotification();
        yield return null;
    }
}
