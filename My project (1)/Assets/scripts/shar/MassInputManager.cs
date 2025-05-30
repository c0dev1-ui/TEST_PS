using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TripleInputManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField massInputField;       // Поле ввода массы объекта
    public TMP_InputField angleInputField;      // Поле ввода угла наклона
    public TMP_InputField thirdInputField;      // Универсальное поле для дополнительного параметра

    [Header("Error Texts")]
    public TextMeshProUGUI massErrorText;       // Текст ошибки для массы
    public TextMeshProUGUI angleErrorText;      // Текст ошибки для угла
    public TextMeshProUGUI thirdErrorText;      // Текст ошибки для третьего параметра

    [Header("Default Values")]
    public float defaultMass = 1f;              // Значение массы по умолчанию
    public float defaultAngle = 30f;            // Значение угла по умолчанию
    public float defaultThirdValue = 10f;       // Значение третьего параметра по умолчанию

    [Header("Scene to Load")]
    public string sceneToLoad = "2yroven"; // Имя сцены для загрузки после валидации

    private void Start()
    {
        // Загрузка сохраненных значений или значений по умолчанию
        float savedMass = PlayerPrefs.GetFloat("BallMass", defaultMass);
        // Форматирование с двумя знаками после запятой
        massInputField.text = savedMass.ToString("0.##");  

        float savedAngle = PlayerPrefs.GetFloat("CubeAngle", defaultAngle);
        angleInputField.text = savedAngle.ToString("0.##");

        float savedThird = PlayerPrefs.GetFloat("ThirdValue", defaultThirdValue);
        thirdInputField.text = savedThird.ToString("0.##");

        // Очистка сообщений об ошибках при старте
        ClearAllErrors();
    }

    // Обработчик нажатия кнопки "Старт"
public void OnStartButtonClicked()
{
    bool hasError = false;

    ClearAllErrors();

    // Проверка массы
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

    // Проверка угла
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

    // Проверка третьего значения
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

    // Защита от перехода
    if (hasError)
    {
        Debug.LogWarning("Ошибка ввода — переход в сцену отменён.");
        return;
    }

    // Сохранение данных
    PlayerPrefs.SetFloat("BallMass", mass);
    PlayerPrefs.SetFloat("CubeAngle", angle);
    PlayerPrefs.SetFloat("ThirdValue", thirdValue);

    // Переход к сцене
    if (!string.IsNullOrEmpty(sceneToLoad))
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    else
    {
        Debug.LogWarning("Имя сцены для загрузки не задано!");
    }
}


    // Метод очистки всех сообщений об ошибках
    void ClearAllErrors()
    {
        massErrorText.text = "";
        angleErrorText.text = "";
        thirdErrorText.text = "";
    }
}