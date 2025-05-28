using UnityEngine;

public class SettingsMenuOpener : MonoBehaviour
{
    public GameObject settingsMenu;
    public string menuObjectName = "SettingMenu"; // имя объекта в иерархии

    void Awake()
    {
        if (settingsMenu == null)
        {
            GameObject found = GameObject.Find(menuObjectName);
            if (found != null) settingsMenu = found;
        }
    }

    public void OpenSettings()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(true);
    }
}
