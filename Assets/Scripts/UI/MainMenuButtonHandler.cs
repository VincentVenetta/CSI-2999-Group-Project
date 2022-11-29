using UnityEngine;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        MenuSystem.currentMenu = mainMenu;
    }

    ///<Summary>Begin the game from level one.</Summary>
    public void OnPlayButtonPressed()
    {
        levelLoader.LoadNextLevel();
    }

    ///<Summary>Switch current menu to level select.</Summary>
    public void OnLevelSelectButtonPressed()
    {
        MenuSystem.SwitchMenu(levelMenu);
    }

    ///<Summary>Switch current menu to credits.</Summary>
    public void OnCreditsButtonPressed()
    {
        MenuSystem.SwitchMenu(creditsMenu);
    }

    ///<Summary>Switch current menu to settings.</Summary>
    public void OnSettingsButtonPressed()
    {
        MenuSystem.SwitchMenu(settingsMenu);
    }

    ///<Summary>Return to the main menu.</Summary>
    public void OnBackButtonPressed()
    {
        MenuSystem.SwitchMenu(mainMenu);
    }

    ///<Summary>Closes the game.</Summary>
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    ///<Summary>Play button clicking noise one.</Summary>
    public void PlayClickOne()
    {
        audioManager.Play("Click One");
    }

    ///<Summary>Play button clicking noise two.</Summary>
    public void PlayClickTwo()
    {
        audioManager.Play("Click Two");
    }

    ///<Summary>Play button clicking noise three.</Summary>
    public void PlayClickThree()
    {
        audioManager.Play("Click Three");
    }
}
