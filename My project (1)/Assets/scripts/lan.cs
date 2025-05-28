using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SFB;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    public Image avatarImage;
    public Button changeImageButton;
    public TMP_Dropdown languageDropdown;

    private void Start()
    {
        Debug.Log("SettingsMenu запущен");

        changeImageButton.onClick.AddListener(ChangeAvatar);
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);

        // Очистка и добавление языков
        languageDropdown.options.Clear();
        languageDropdown.options.Add(new TMP_Dropdown.OptionData("Русский"));
        languageDropdown.options.Add(new TMP_Dropdown.OptionData("English"));

        // Установка текущего языка
        string savedLang = PlayerPrefs.GetString("language", "ru");
        int index = savedLang == "ru" ? 0 : 1;
        languageDropdown.value = index;
        languageDropdown.RefreshShownValue();

        StartCoroutine(SetLocale(savedLang));
    }

    void ChangeAvatar()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var paths = StandaloneFileBrowser.OpenFilePanel("Выберите изображение", "", new[] {
            new ExtensionFilter("Изображения", "png", "jpg", "jpeg")
        }, false);

        if (paths.Length > 0 && File.Exists(paths[0]))
        {
            byte[] imageData = File.ReadAllBytes(paths[0]);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            Sprite newSprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            avatarImage.sprite = newSprite;
        }
#endif
    }

    void ChangeLanguage(int index)
    {
        string langCode = index == 0 ? "ru" : "en";
        PlayerPrefs.SetString("language", langCode);
        StartCoroutine(SetLocale(langCode));
    }

    IEnumerator SetLocale(string langCode)
    {
        yield return LocalizationSettings.InitializationOperation;

        var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == langCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            Debug.Log("Язык установлен: " + langCode);
        }
    }
}
