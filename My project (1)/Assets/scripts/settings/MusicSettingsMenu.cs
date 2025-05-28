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
    public AudioSource audioSource;

    private void Start()
    {
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        selectMusicButton.onClick.AddListener(OpenFileDialog);
    }

    private void OnVolumeChanged(float value)
    {
        audioSource.volume = value;
    }

    private void OpenFileDialog()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var paths = StandaloneFileBrowser.OpenFilePanel("Выберите аудиофайл", "", new[] {
            new ExtensionFilter("Аудиофайлы", "mp3", "wav", "ogg")
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

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
#else
            if (www.isNetworkError || www.isHttpError)
#endif
            {
                Debug.LogError("Ошибка загрузки аудио: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    private AudioType GetAudioTypeFromExtension(string extension)
    {
        extension = extension.ToLower();
        switch (extension)
        {
            case ".mp3": return AudioType.MPEG;
            case ".wav": return AudioType.WAV;
            case ".ogg": return AudioType.OGGVORBIS;
            default: return AudioType.UNKNOWN;
        }
    }
}
