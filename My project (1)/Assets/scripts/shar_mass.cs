using UnityEngine;

public class BallMassTest : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float mass = PlayerPrefs.GetFloat("PlayerMass", 1f);
        rb.mass = mass;

        Debug.Log("Установлена масса: " + mass);

        // Применим силу, чтобы видно было разницу
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
    }
}
