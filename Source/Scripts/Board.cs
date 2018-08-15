using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Board : MonoBehaviour {
	public Transform Tile;
	public Transform BombTile;
	public List<Transform> InnerTile;
	public int size;
	public int bombNum;
	private int unopened;
	public GameObject[,] boardview; // menyimpan gameobject yang ditampilkan
	public int[,] boardlogic; // menyimpan logic dari gameboard (negatif: panel bom, 0-8: jumlah bom di sekitar panel)
	public bool[,] boardstates; // Menyimpan state gameboard (true: bisa diklik, false: tidak)
	private int victoryMessageDelay; //memastikan board selesai di-reveal sebelum message muncul
	
	// Use this for initialization
	void Start () {
		victoryMessageDelay = 5;
		unopened = size * size;
		boardview = new GameObject[size, size];
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++){
				boardview[i,j] = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity).gameObject;
			}
		}
		boardlogic = new int[size, size];
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++){
				boardlogic[i,j] = 0;
			}
		}
		for (int i = 0; i < bombNum; i++) {
			int tempX = Random.Range(0,size);
			int tempY = Random.Range(0,size);
			if (boardlogic[tempX, tempY] >= 0){ // bukan bomb
				boardlogic[tempX,tempY] = -10; // jadikan bomb 
				MarkSurrounding(tempX, tempY); 
			} else { // sudah bomb
				i--; //ulangi cari tempat random
			}
		}
		boardstates = new bool[size,size];
		for(int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++){
				boardstates[i,j] = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (unopened == bombNum) {
			RevealAllMines();
			for (int i = 0; i < size; i++){
				for (int j = 0; j < size; j++) {
					if (boardlogic[i,j] < 0)
						boardview[i,j].GetComponent<SpriteRenderer>().color = Color.yellow;
				}
			}
		}
		if (unopened == 0) {
			victoryMessageDelay -= 1;
			if (victoryMessageDelay == 0) {
				this.GetComponent<Dialog>().ShowDialog("Congratulations", "Selamat, anda menang!", "Main lagi");
			}
		}
	}

	public void Reveal(int i, int j) {
		if (boardstates[i,j]) {
			unopened -= 1;
			boardstates[i,j] = false;
			if (boardlogic[i,j] < 0) {
				boardview[i,j].GetComponent<SpriteRenderer>().sprite = BombTile.gameObject.GetComponent<SpriteRenderer>().sprite;
			} else {
				boardview[i,j].GetComponent<SpriteRenderer>().sprite = InnerTile[boardlogic[i,j]].gameObject.GetComponent<SpriteRenderer>().sprite;
			}
		}
	}

	public void RevealAll() {
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Reveal(i, j);				
			}
		}
	}

	public void RevealAllMines() {
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				if (boardlogic[i,j] < 0) Reveal(i, j);				
			}
		}
	}

	private void MarkSurrounding(int X, int Y)
	{	// menambah satu ke semua panel di sekeliling bomb
		bool left = X == 0;
		bool right = X == size - 1;
		bool top = Y == size - 1;
		bool bottom = Y == 0;

		if (!top)
		{
			boardlogic[X, Y + 1] += 1;
			if (!left)
			{
				boardlogic[X - 1, Y + 1] += 1;
			}
			if (!right)
			{
				boardlogic[X + 1, Y + 1] += 1;
			}
		}
		if (!bottom)
		{
			boardlogic[X, Y - 1] += 1;
			if (!left)
			{
				boardlogic[X - 1, Y - 1] += 1;
			}
			if (!right)
			{
				boardlogic[X + 1, Y - 1] += 1;
			}
		}
		if (!left)
		{
			boardlogic[X - 1, Y] += 1;
		}
		if (!right)
		{
			boardlogic[X + 1, Y] += 1;
		}
	}
}
