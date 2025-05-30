using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using SFB;

public class MusicSettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button selectMusicButton;
    public Button selectBackgroundButton;  // Новая кнопка
    public RawImage backgroundImage;       // Объект, где будет фон

    private void Start()
    {
        if (MusicPlayer.Instance != null)
        {
            volumeSlider.value = MusicPlayer.Instance.audioSource.volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            selectMusicButton.onClick.AddListener(OpenMusicFileDialog);
        }
        else
        {
            Debug.LogError("MusicPlayer not found!");
        }

        // Подключаем обработчик кнопки выбора фона
        if (selectBackgroundButton != null)
        {
            selectBackgroundButton.onClick.AddListener(OpenImageFileDialog);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.audioSource.volume = value;
    }

    private void OpenMusicFileDialog()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var paths = StandaloneFileBrowser.OpenFilePanel("Выберите аудиофайл", "", new[] {
            new ExtensionFilter("Audio Files", "mp3", "wav", "ogg")
        }, false);

        if (paths.Length > 0 && File.Exists(paths[0]))
        {
            StartCoroutine(LoadAndPlayAudio(paths[0]));
        }
#endif
    }

    private IEnumerator LoadAndPlayAudio(string path)
    {
        string url = "file:///" + path.Replace("\\", "/");
        AudioType audioType = GetAudioTypeFromExtension(Path.GetExtension(path));

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                MusicPlayer.Instance.audioSource.clip = clip;
                MusicPlayer.Instance.audioSource.Play();
            }
        }
    }

    private AudioType GetAudioTypeFromExtension(string ext)
    {
        ext = ext.ToLower();
        switch (ext)
        {
            case ".mp3": return AudioType.MPEG;
            case ".wav": return AudioType.WAV;
            case ".ogg": return AudioType.OGGVORBIS;
            default: return AudioType.UNKNOWN;
        }
    }

    private void OpenImageFileDialog()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var paths = StandaloneFileBrowser.OpenFilePanel("Выберите изображение", "", new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
        }, false);

        if (paths.Length > 0 && File.Exists(paths[0]))
        {
            StartCoroutine(LoadAndSetImage(paths[0]));
        }
#endif
    }

    private IEnumerator LoadAndSetImage(string path)
    {
        string url = "file:///" + path.Replace("\\", "/");

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки изображения: " + www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                if (backgroundImage != null)
                {
                    backgroundImage.texture = texture;
                }
            }
        }
    }
}
