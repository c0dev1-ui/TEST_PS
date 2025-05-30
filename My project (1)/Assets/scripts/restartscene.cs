using UnityEngine;
using UnityEngine.SceneManagement;  // Нужно для управления сценами

public class RestartSceneButton : MonoBehaviour
{
    public void RestartScene()
    {
        // Получаем текущую активную сцену
        Scene currentScene = SceneManager.GetActiveScene();
        // Перезапускаем её
        SceneManager.LoadScene(currentScene.name);
    }
}
