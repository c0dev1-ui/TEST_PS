using UnityEngine;

public class CubeMassAndFriction : MonoBehaviour
{
    private Rigidbody rb;

    public float forceAmount = 10f;      // Начальная сила вперёд
    public float frictionStrength = 1f;  // "Трение" — чем больше, тем быстрее тормозит

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float mass = PlayerPrefs.GetFloat("PlayerMass", 1f);
        rb.mass = mass;

        rb.freezeRotation = true; // Отключить вращение

        Debug.Log("Масса куба установлена: " + mass);

        // Применим силу вперёд
        rb.AddForce(Vector3.forward * forceAmount, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Имитация трения — уменьшаем скорость
        Vector3 velocity = rb.linearVelocity;
        velocity.x *= (1 - Time.fixedDeltaTime * frictionStrength);
        rb.linearVelocity = velocity;
    }
}
