using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MassInputManager : MonoBehaviour
{
    public TMP_InputField massInputField;
    public GameObject errorPanel;
    public TextMeshProUGUI errorText;
    void Start()
    {
    // Установим значение по умолчанию
        massInputField.text = "1";
    }

    public void OnStartButtonClicked()
    {
        string input = massInputField.text;
        float mass;

        if (!float.TryParse(input, out mass))
        {
            ShowError("Введите число.");
            return;
        }

        if (mass <= 0)
        {
            ShowError("Масса должна быть положительной.");
            return;
        }

        PlayerPrefs.SetFloat("BallMass", mass);
        SceneManager.LoadScene("SimulationScene");
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
