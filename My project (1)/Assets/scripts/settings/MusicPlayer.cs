using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // сохраняем при переходе между сценами
        }
        else
        {
            Destroy(gameObject); // удаляем дубликат, если уже есть
        }
    }
}
