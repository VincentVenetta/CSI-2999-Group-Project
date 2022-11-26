using UnityEngine;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;
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
}
