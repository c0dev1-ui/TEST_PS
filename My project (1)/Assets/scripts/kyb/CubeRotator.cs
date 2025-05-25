using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    void Start()
    {
        float angle = PlayerPrefs.GetFloat("CubeAngle", 30f);
        transform.rotation = Quaternion.Euler(angle, 0, 0);
        Debug.Log("Установлен угол наклона куба: " + angle);
    }
}
