using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlasmaUIManager : MonoBehaviour
{
    public TMP_InputField particleCountInput;
    public TMP_InputField attractionInput;
    public TMP_InputField repulsionInput;
    public TMP_Text errorText;

    private const int sceneToLoad = 1; // Номер сцены с плазмой

    public void OnStartButtonClick()
    {
        int particleCount;
        float attraction, repulsion;

        // Проверка и парсинг количества частиц
        if (!int.TryParse(particleCountInput.text, out particleCount) || particleCount <= 0)
        {
            errorText.text = "Введите корректное количество частиц";
            return;
        }

        // Проверка и парсинг силы притяжения
        if (!float.TryParse(attractionInput.text, out attraction) || attraction <= 0)
        {
            errorText.text = "Введите корректную силу притяжения";
            return;
        }

        // Проверка и парсинг силы отталкивания
        if (!float.TryParse(repulsionInput.text, out repulsion) || repulsion <= 0)
        {
            errorText.text = "Введите корректную силу отталкивания";
            return;
        }

        // Сохраняем параметры в PlayerPrefs
        PlayerPrefs.SetInt("ParticleCount", particleCount);
        PlayerPrefs.SetFloat("AttractionForce", attraction);
        PlayerPrefs.SetFloat("RepulsionForce", repulsion);

        // Загружаем следующую сцену
        SceneManager.LoadScene(sceneToLoad);
    }
}
