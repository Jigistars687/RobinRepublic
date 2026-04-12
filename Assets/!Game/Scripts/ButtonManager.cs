using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _gameplayButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resetAllButton;

    [SerializeField] private Button _BackToGameButton;

    [SerializeField] private PlayerInputsControler _playerInputsControler;

    private string _gameplay = "GameScene";
    private string _mainMenu = "MainMenu";

    private void OnEnable()
    {
        if (_gameplayButton != null) _gameplayButton.onClick.AddListener(() => LoadScene(_gameplay));
        if (_mainMenuButton != null) _mainMenuButton.onClick.AddListener(() => LoadScene(_mainMenu));
        if (_resetAllButton != null) _resetAllButton.onClick.AddListener(ResetScene);
        if (_exitButton != null) _exitButton.onClick.AddListener(QuitGame);
        if (_BackToGameButton != null || _playerInputsControler != null) 
            _BackToGameButton.onClick.AddListener(() => _playerInputsControler.PauseGame());
    }

    private void OnDisable()
    {
        if (_gameplayButton != null) _gameplayButton.onClick.RemoveAllListeners(); 
        if ( _mainMenuButton != null) _mainMenuButton.onClick.RemoveAllListeners();
        if (_exitButton != null) _exitButton.onClick.RemoveAllListeners();
        if (_resetAllButton != null) _resetAllButton.onClick.RemoveAllListeners();
        if (_BackToGameButton != null || _playerInputsControler != null) 
            _BackToGameButton.onClick.RemoveAllListeners();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}