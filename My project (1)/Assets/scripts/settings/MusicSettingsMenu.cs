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

    private void Start()
    {
        if (MusicPlayer.Instance != null)
        {
            volumeSlider.value = MusicPlayer.Instance.audioSource.volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            selectMusicButton.onClick.AddListener(OpenFileDialog);
        }
        else
        {
            Debug.LogError("MusicPlayer not found!");
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.audioSource.volume = value;
    }

    private void OpenFileDialog()
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
}
