using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour {

	private static GameView instance = null;

	[SerializeField]
	private Sprite[] cellSprite;

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

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		Destroy(this.gameObject);
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
