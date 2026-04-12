using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Точное название сцены, которую нужно запустить")]
    public string _sceneToLoad = "GameScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}