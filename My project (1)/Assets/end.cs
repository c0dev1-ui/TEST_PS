using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Для билда (собранного приложения)
        Application.Quit();
        
        // Для остановки в редакторе Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}