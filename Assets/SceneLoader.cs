using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }
}