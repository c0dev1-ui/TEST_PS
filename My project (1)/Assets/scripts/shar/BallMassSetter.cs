using UnityEngine;

public class BallMassSetter : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("На объекте нет Rigidbody!");
            return;
        }

        // Получаем массу из предыдущей сцены
        float mass = PlayerPrefs.GetFloat("BallMass", 1f);
        Debug.Log("Применяем массу: " + mass);

        // Устанавливаем массу
        rb.mass = mass;

        // Применим силу вперёд — чем меньше масса, тем дальше полетит
        rb.AddForce(Vector3.forward * 100f, ForceMode.Impulse);
    }
}
