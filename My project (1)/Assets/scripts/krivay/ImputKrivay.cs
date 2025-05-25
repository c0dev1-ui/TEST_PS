using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DroneInputManager : MonoBehaviour
{
    public TMP_InputField speedInputField;
    public TMP_InputField amplitudeInputField;
    public TMP_InputField frequencyInputField;
    public GameObject errorPanel;
    public TextMeshProUGUI errorText;

    void Start()
    {
        speedInputField.text = PlayerPrefs.GetFloat("DroneSpeed", 2f).ToString("0.##");
        amplitudeInputField.text = PlayerPrefs.GetFloat("DroneAmplitude", 1f).ToString("0.##");
        frequencyInputField.text = PlayerPrefs.GetFloat("DroneFrequency", 1f).ToString("0.##");
    }

    public void OnStartButtonClicked()
    {
        float speed, amplitude, frequency;

        if (!float.TryParse(speedInputField.text, out speed) || speed <= 0)
        {
            ShowError("Введите положительное значение скорости.");
            return;
        }

        if (!float.TryParse(amplitudeInputField.text, out amplitude) || amplitude < 0)
        {
            ShowError("Амплитуда должна быть неотрицательной.");
            return;
        }

        if (!float.TryParse(frequencyInputField.text, out frequency) || frequency < 0)
        {
            ShowError("Частота должна быть неотрицательной.");
            return;
        }

        PlayerPrefs.SetFloat("DroneSpeed", speed);
        PlayerPrefs.SetFloat("DroneAmplitude", amplitude);
        PlayerPrefs.SetFloat("DroneFrequency", frequency);

        SceneManager.LoadScene("DroneScene");
    }

    void ShowError(string msg)
    {
        errorText.text = msg;
        errorPanel.SetActive(true);
    }

    public void OnCloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}
