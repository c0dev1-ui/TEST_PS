using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BouncingBallMenu : MonoBehaviour
{
    public TMP_InputField elasticityInput; // Поле ввода коэффициента упругости
    public TextMeshProUGUI errorText;      // Текст для отображения ошибок

    [Tooltip("Номер сцены с мячом")]
    public int ballSceneNumber = 1;        // Индекс сцены с симуляцией мяча

    // Допустимые границы коэффициента упругости
    private const float MinElasticity = 0f;
    private const float MaxElasticity = 1f;

    private void Start()
    {
        elasticityInput.text = "0.8";  // Установка значения по умолчанию
        errorText.text = "";            // Очистка текста ошибки
    }

    public void OnStartBallScene()
    {
        errorText.text = ""; // Сброс предыдущих ошибок

        // Парсинг введенного значения
        if (!float.TryParse(elasticityInput.text, out float elasticity))
        {
            errorText.text = $"Ошибка: коэффициент упругости должен быть числом от {MinElasticity} до {MaxElasticity}";
            return;
        }

        // Проверка диапазона значения
        if (elasticity < MinElasticity || elasticity > MaxElasticity)
        {
            errorText.text = $"Ошибка: коэффициент упругости должен быть от {MinElasticity} до {MaxElasticity}";
            return;
        }

        // Проверка валидности номера сцены
        if (ballSceneNumber < 0 || ballSceneNumber >= SceneManager.sceneCountInBuildSettings)
        {
            errorText.text = $"Ошибка: номер сцены {ballSceneNumber} вне диапазона";
            return;
        }

        PlayerPrefs.SetFloat("BallElasticity", elasticity); // Сохранение значения

        SceneManager.LoadScene(ballSceneNumber); // Загрузка сцены с мячом
    } // Конец метода OnStartBallScene
} // Конец класса