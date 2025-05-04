using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;
    public Animator cameraAnimator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            ShowPauseMenu();
        }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        ShowUI();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        HideUI();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void HideUI()
    {
        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
    }
    private void ShowUI()
    {
        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}