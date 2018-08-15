using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour {
	public GameObject board;
	public int sizeinput, bombinput;
	// Use this for initialization
	void Start () {
	}
	
	public void ValidateAndPlay () {
		string a = GameObject.FindWithTag("sizeinput").GetComponentInChildren<Text>().text;
		string b = GameObject.FindWithTag("bombinput").GetComponentInChildren<Text>().text;
		bool valid1 = Int32.TryParse(a, out sizeinput);
		bool valid2 = Int32.TryParse(b, out bombinput);
		if (valid1 && valid2 && sizeinput*sizeinput > bombinput && bombinput > 0) {
			GameObject.FindWithTag("canvas").SetActive(false);
			CreateBoard();
		} else {
			GetComponent<Dialog>().ShowDialog("Invalid Input", "Ada input anda yang tidak valid.", "Coba lagi");
		}
	}

	private void CreateBoard(){
		Board script = board.GetComponent<Board>();
		script.size = sizeinput;
		script.bombNum = bombinput;
		int boardpos = sizeinput/2;
		GameObject.FindWithTag("MainCamera").transform.position = new Vector3(boardpos, boardpos, -10);
		board = Instantiate(board, new Vector3(boardpos, boardpos, 0), Quaternion.identity);
	}

	public void Quit() {
		Application.Quit();
	}
}
