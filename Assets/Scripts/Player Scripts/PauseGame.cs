using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private AudioManager audioManager;
    public bool isPaused;

    private void Awake()
    {
        isPaused = false;
    }

    private void Update()
    {
        Pause();
    }

    private void Pause()
    {
        if (inputManager.pausePressed)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                isPaused = true;

                //Lock sfx until level 4
                if (SceneManager.GetActiveScene().buildIndex >= 4)
                {                   
                    audioManager.Play("Pause");
                }
            }

            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                isPaused = false;

                //Lock sfx until level 4
                if (SceneManager.GetActiveScene().buildIndex >= 4)
                {                   
                    audioManager.Play("Unpause");
                }
            }
        }
    }
}
