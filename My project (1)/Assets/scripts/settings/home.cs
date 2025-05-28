using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0); // Заменить на имя сцены, если нужно
    }
}
