using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public TMP_InputField inputForce1;
    public TMP_InputField inputForce2;
    public TextMeshProUGUI errorText;

    [Tooltip("Номер сцены для загрузки")]
    public int sceneNumber = 3;

    private const float MinForce = -100f;
    private const float MaxForce = 100f;

    public void StartMagnetScene()
    {
        errorText.text = "";

        if (!TryParseAndValidateForce(inputForce1.text, out float f1))
        {
            errorText.text = $"Ошибка: сила магнита 1 должна быть числом от {MinForce} до {MaxForce}";
            return;
        }

        if (!TryParseAndValidateForce(inputForce2.text, out float f2))
        {
            errorText.text = $"Ошибка: сила магнита 2 должна быть числом от {MinForce} до {MaxForce}";
            return;
        }

        if (sceneNumber < 0 || sceneNumber >= SceneManager.sceneCountInBuildSettings)
        {
            errorText.text = $"Ошибка: номер сцены {sceneNumber} вне диапазона";
            return;
        }

        PlayerPrefs.SetFloat("Magnet1Force", f1);
        PlayerPrefs.SetFloat("Magnet2Force", f2);

        SceneManager.LoadScene(sceneNumber);
    }

    private bool TryParseAndValidateForce(string input, out float value)
    {
        if (float.TryParse(input, out value))
        {
            if (value >= MinForce && value <= MaxForce)
                return true;
        }

        value = 0f;
        return false;
    }
}
