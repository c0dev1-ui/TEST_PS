using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // обязательно для TMP_InputField

public class MassInputManager : MonoBehaviour
{
    public TMP_InputField inputField;

    public void OnPlayButtonClicked()
    {
        if (float.TryParse(inputField.text, out float mass))
        {
            PlayerPrefs.SetFloat("PlayerMass", mass);
            SceneManager.LoadScene("GameScene"); // замени на название своей сцены
        }
        else
        {
            Debug.LogWarning("Введите корректное число!");
        }
    }
}
