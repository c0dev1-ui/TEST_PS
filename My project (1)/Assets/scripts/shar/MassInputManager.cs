using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TripleInputManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField massInputField;
    public TMP_InputField angleInputField;
    public TMP_InputField thirdInputField;  // Третье поле

    [Header("Error Texts")]
    public TextMeshProUGUI massErrorText;
    public TextMeshProUGUI angleErrorText;
    public TextMeshProUGUI thirdErrorText;

    [Header("Default Values")]
    public float defaultMass = 1f;
    public float defaultAngle = 30f;
    public float defaultThirdValue = 10f;

    [Header("Scene to Load")]
    public string sceneToLoad = "SimulationScene";  // Здесь задаёшь сцену в инспекторе или программно

    private void Start()
    {
        float savedMass = PlayerPrefs.GetFloat("BallMass", defaultMass);
        massInputField.text = savedMass.ToString("0.##");

        float savedAngle = PlayerPrefs.GetFloat("CubeAngle", defaultAngle);
        angleInputField.text = savedAngle.ToString("0.##");

        float savedThird = PlayerPrefs.GetFloat("ThirdValue", defaultThirdValue);
        thirdInputField.text = savedThird.ToString("0.##");

        ClearAllErrors();
    }

    public void OnStartButtonClicked()
    {
        bool hasError = false;

        ClearAllErrors();

        if (!float.TryParse(massInputField.text, out float mass))
        {
            massErrorText.text = "Масса — это число.";
            hasError = true;
        }
        else if (mass <= 0)
        {
            massErrorText.text = "Масса должна быть положительной.";
            hasError = true;
        }

        if (!float.TryParse(angleInputField.text, out float angle))
        {
            angleErrorText.text = "Угол — это число.";
            hasError = true;
        }
        else if (angle < 0 || angle > 90)
        {
            angleErrorText.text = "Угол должен быть от 0 до 90.";
            hasError = true;
        }

        if (!float.TryParse(thirdInputField.text, out float thirdValue))
        {
            thirdErrorText.text = "Значение — это число.";
            hasError = true;
        }
        else if (thirdValue <= 0)
        {
            thirdErrorText.text = "Значение должно быть положительным.";
            hasError = true;
        }

        if (hasError)
        {
            return;
        }

        PlayerPrefs.SetFloat("BallMass", mass);
        PlayerPrefs.SetFloat("CubeAngle", angle);
        PlayerPrefs.SetFloat("ThirdValue", thirdValue);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Имя сцены для загрузки не задано!");
        }
    }

    void ClearAllErrors()
    {
        massErrorText.text = "";
        angleErrorText.text = "";
        thirdErrorText.text = "";
    }
}
