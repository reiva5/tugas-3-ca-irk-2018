using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Board board;
	private int posX, posY;
	// Use this for initialization
	void Start () {
		board = GameObject.FindWithTag("Board").GetComponent<Board>();
		posX = (int) this.transform.position.x;
		posY = (int) this.transform.position.y;
	}
	
	// Update is called once per frame

	void OnMouseDown() {
		if (board.boardstates[posX, posY]) {
			if (board.boardlogic[posX, posY] < 0) {
				board.RevealAllMines();
				GetComponent<SpriteRenderer>().color = Color.red;
				board.GetComponent<Dialog>().ShowDialog("Game Over", "Anda Kalah", "Main lagi");
			} else {
				board.Reveal(posX, posY);
			}
			if (board.boardlogic[posX, posY] == 0) {
				OpenSurrounding();
			}
		}
	}

	void OpenSurrounding() {
		int X = posX;
		int Y = posY;
		bool left = X == 0;
		bool right = X == board.size - 1;
		bool top = Y == board.size - 1;
		bool bottom = Y == 0;

		if (!top)
		{
			board.boardview[X, Y + 1].GetComponent<Tile>().OnMouseDown();
			if (!left)
			{
				board.boardview[X - 1, Y + 1].GetComponent<Tile>().OnMouseDown();
			}
			if (!right)
			{
				board.boardview[X + 1, Y + 1].GetComponent<Tile>().OnMouseDown();
			}
		}
		if (!bottom)
		{
			board.boardview[X, Y - 1].GetComponent<Tile>().OnMouseDown();
			if (!left)
			{
				board.boardview[X - 1, Y - 1].GetComponent<Tile>().OnMouseDown();
			}
			if (!right)
			{
				board.boardview[X + 1, Y - 1].GetComponent<Tile>().OnMouseDown();
			}
		}
		if (!left)
		{
			board.boardview[X - 1, Y].GetComponent<Tile>().OnMouseDown();
		}
		if (!right)
		{
			board.boardview[X + 1, Y].GetComponent<Tile>().OnMouseDown();
		}
	}

}
