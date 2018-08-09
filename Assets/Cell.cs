using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    [SerializeField]
    private bool isThisMine;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CellClick()
    {
        if (BoardController.Instance.IsWaitingForInput())
        {
            BoardController.Instance.CellClick(this.transform.GetSiblingIndex());
        }
    }

    public void SetImage(Sprite sprite) {
        GetComponent<Image>().sprite = sprite;
    }
}
