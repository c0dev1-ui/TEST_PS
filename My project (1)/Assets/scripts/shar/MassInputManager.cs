using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MassInputManager : MonoBehaviour
{
    public TMP_InputField massInputField;      // Поле ввода массы
    public TMP_InputField angleInputField;     // Поле ввода угла

    public TextMeshProUGUI massErrorText;      // Ошибка по массе
    public TextMeshProUGUI angleErrorText;     // Ошибка по углу

    void Start()
    {
        // Установка сохранённых значений по умолчанию
        massInputField.text = PlayerPrefs.GetFloat("BallMass", 1f).ToString("0.##");
        angleInputField.text = PlayerPrefs.GetFloat("CubeAngle", 30f).ToString("0.##");
        ClearErrors();
    }

    public void OnStartButtonClicked()
    {
        ClearErrors();

        float mass, angle;
        bool hasError = false;

        // Проверка массы
        if (!float.TryParse(massInputField.text, out mass))
        {
            massErrorText.text = "Масса должна быть числом.";
            hasError = true;
        }
        else if (mass <= 0)
        {
            massErrorText.text = "Масса должна быть положительной.";
            hasError = true;
        }

        // Проверка угла
        if (!float.TryParse(angleInputField.text, out angle))
        {
            angleErrorText.text = "Угол должен быть числом.";
            hasError = true;
        }
        else if (angle < 0 || angle > 90)
        {
            angleErrorText.text = "Угол должен быть от 0 до 90.";
            hasError = true;
        }

        if (hasError) return;

        // Сохранение и переход к сцене
        PlayerPrefs.SetFloat("BallMass", mass);
        PlayerPrefs.SetFloat("CubeAngle", angle);
        SceneManager.LoadScene("SimulationScene");
    }

    void ClearErrors()
    {
        massErrorText.text = "";
        angleErrorText.text = "";
    }
}
