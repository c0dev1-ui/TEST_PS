using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BouncingBallMenu : MonoBehaviour
{
    public TMP_InputField elasticityInput;
    public TextMeshProUGUI errorText;

    [Tooltip("Номер сцены с мячом")]
    public int ballSceneNumber = 1;

    private const float MinElasticity = 0f;
    private const float MaxElasticity = 1f;

    private void Start()
    {
        elasticityInput.text = "0.8";  // Значение по умолчанию
        errorText.text = "";
    }

    public void OnStartBallScene()
    {
        errorText.text = "";

        if (!float.TryParse(elasticityInput.text, out float elasticity))
        {
            errorText.text = $"Ошибка: коэффициент упругости должен быть числом от {MinElasticity} до {MaxElasticity}";
            return;
        }

        if (elasticity < MinElasticity || elasticity > MaxElasticity)
        {
            errorText.text = $"Ошибка: коэффициент упругости должен быть от {MinElasticity} до {MaxElasticity}";
            return;
        }

        if (ballSceneNumber < 0 || ballSceneNumber >= SceneManager.sceneCountInBuildSettings)
        {
            errorText.text = $"Ошибка: номер сцены {ballSceneNumber} вне диапазона";
            return;
        }

        PlayerPrefs.SetFloat("BallElasticity", elasticity);

        SceneManager.LoadScene(ballSceneNumber);
    }
}
