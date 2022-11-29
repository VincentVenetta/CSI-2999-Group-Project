using UnityEngine;

public static class MenuSystem
{
    public static GameObject currentMenu;

    ///<Summary>Opens a menu by enabling the root game object.</Summary>
    public static void OpenMenu(GameObject _menuToOpen)
    {
        _menuToOpen.SetActive(true);
    }

    ///<Summary>Closes a menu by disabling the root game object.</Summary>
    public static void CloseMenu(GameObject _menuToClose)
    {
        _menuToClose.SetActive(false);
    }

    ///<Summary>Switches the currently active menu game object.</Summary>
    public static void SwitchMenu(GameObject _menuToSwitchTo)
    {
        OpenMenu(_menuToSwitchTo);
        CloseMenu(currentMenu);
        currentMenu = _menuToSwitchTo;
    }

    ///<Summary>Get current menu game object.</Summary>
    public static GameObject GetCurrentMenu()
    {
        return currentMenu;
    }
}
