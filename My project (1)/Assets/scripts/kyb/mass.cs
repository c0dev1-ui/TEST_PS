using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // обязательно для TMP_InputField

public class MassInputManagerd : MonoBehaviour
{
    public TMP_InputField inputField;

    public void OnPlayButtonClicked()
    {
        if (float.TryParse(inputField.text, out float mass))
        {
            PlayerPrefs.SetFloat("PlayerMass", mass);
            SceneManager.LoadScene("GameScene"); 
        }
        else
        {
            Debug.LogWarning("Введите корректное число!");
        }
    }
}
