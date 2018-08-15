using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Dialog : MonoBehaviour
{
     // 200x300 px window will apear in the center of the screen.
     private Rect windowRect = new Rect ((Screen.width - 200)/2, (Screen.height - 300)/2, 200, 100);
     // Only show it if needed.
     private bool show = false;
     private string title, text, button;
    
    void OnGUI () 
    {
        if(show)
            windowRect = GUI.Window (0, windowRect, DialogWindow, title);
        
    }

    // This is the actual window.
    void DialogWindow (int windowID)
    {
        GUI.Label(new Rect(5,20, windowRect.width, 20), text);

        if(GUI.Button(new Rect(5,40, windowRect.width - 10, 20), button))
        {
            show = false;
            windowRect = new Rect ((Screen.width - 200)/2, (Screen.height - 300)/2, 200, 100);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    // To open the dialogue from outside of the script.
    public void ShowDialog(string tit, string txt, string butt)
    {
        title = tit;
        text = txt;
        button = butt;
        show = true;
    }
}