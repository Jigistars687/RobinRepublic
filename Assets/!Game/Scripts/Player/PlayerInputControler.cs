using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsControler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mainUI;
     private bool _isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        _isPaused = !_isPaused;
        _pauseMenu.SetActive(_isPaused);
        _mainUI.SetActive(!_isPaused);
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isPaused;

    }
}
