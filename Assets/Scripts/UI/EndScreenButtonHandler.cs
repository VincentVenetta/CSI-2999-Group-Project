using UnityEngine;

public class EndScreenButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private AudioManager audioManager;

    public void OnMainMenuButtonPressed()
    {
        audioManager.Play("Click One");
        levelLoader.SwitchLevel(0);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
