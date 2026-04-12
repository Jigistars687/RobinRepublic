using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _gameplayButton;
    [SerializeField] private Button _mainMenuButton;


    private string _gameplay = "GameScene";
    private string _mainMenu = "MainMenu";

    private void OnEnable()
    {
        if (_gameplayButton != null) _gameplayButton.onClick.AddListener(() => LoadScene(_gameplay));
        if (_mainMenuButton != null) _mainMenuButton.onClick.AddListener(() => LoadScene(_mainMenu));
    }

    private void OnDisable()
    {
        if (_gameplayButton != null) _gameplayButton.onClick.RemoveAllListeners(); 
        if ( _mainMenuButton != null) _mainMenuButton.onClick.RemoveAllListeners();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}