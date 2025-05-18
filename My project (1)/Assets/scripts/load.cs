using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
public class load : MonoBehaviour
{
    public void OnLevelWasLoaded(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
