using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MassInputManagerd : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;

    public void OnPlayButtonClicked()
    {
        if (float.TryParse(inputField.text, out float mass))
        {
            if (mass <= 0)
            {
                errorText.text = "Масса должна быть положительной.";
                return;
            }

            PlayerPrefs.SetFloat("PlayerMass", mass);
            errorText.text = ""; // Очистка ошибки
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            errorText.text = "Введите корректное число.";
        }
    }
}
