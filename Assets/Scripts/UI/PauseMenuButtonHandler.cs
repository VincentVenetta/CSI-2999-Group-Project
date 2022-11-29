using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PauseGame pauseGame;

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        levelLoader.SwitchLevel(0);
    }

    public void OnRestartButtonPressed()
    {
        Time.timeScale = 1f;
        levelLoader.ReloadLevel();
    }

    public void OnResumeButtonPressed()
    {
        pauseMenu.SetActive(false);

        //Lock sfx until level 4
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {                   
            audioManager.Play("Unpause");
        }

        pauseGame.isPaused = false;
        Time.timeScale = 1f;
    }
}
