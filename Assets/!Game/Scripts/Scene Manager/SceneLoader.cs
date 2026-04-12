using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoader : MonoBehaviour
{
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void Exit()
    { 
        Application.Quit();
    }
}
