using UnityEngine;

public class GameView : MonoBehaviour
{
    private static GameView instance = null;

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
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
