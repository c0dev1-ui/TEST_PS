using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Magnet : MonoBehaviour
{
    [Tooltip("ID магнита: 1 или 2")]
    public int magnetID = 1;

    [Tooltip("Автоматически задавать массу на основе силы (|force|)")]
    public bool autoMass = true;

    [Tooltip("Максимальное значение массы при автоподборе")]
    public float maxAutoMass = 100f;

    [Tooltip("Минимальная масса (для автонастройки и защиты от деления на ноль)")]
    public float minMass = 1f;

    [HideInInspector]
    public float force = 0f;

    private Rigidbody rb;
    private Magnet otherMagnet;

    private Vector3 lastForceDirection = Vector3.zero;
    private float lastForceMagnitude = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Загрузка силы из PlayerPrefs
        if (magnetID == 1)
            force = PlayerPrefs.GetFloat("Magnet1Force", 0f);
        else if (magnetID == 2)
            force = PlayerPrefs.GetFloat("Magnet2Force", 0f);
        else
            Debug.LogWarning($"[Magnet] Неверный magnetID: {magnetID} на объекте {gameObject.name}");

        // Автоматическая настройка массы
        if (autoMass)
        {
            rb.mass = Mathf.Clamp(Mathf.Abs(force), minMass, maxAutoMass);
        }

        // Поиск другого магнита
        Magnet[] magnets = FindObjectsOfType<Magnet>();
        foreach (var m in magnets)
        {
            if (m != this && m.magnetID != this.magnetID)
            {
                otherMagnet = m;
                break;
            }
        }

        if (otherMagnet == null)
            Debug.LogWarning($"[Magnet] Не найден другой магнит для {gameObject.name}");

        Debug.Log($"[{gameObject.name}] Сила: {force}, Масса: {rb.mass}");
    }

    void FixedUpdate()
    {
        if (otherMagnet == null || Mathf.Approximately(force, 0f) || Mathf.Approximately(otherMagnet.force, 0f))
            return;

        Vector3 toOther = otherMagnet.transform.position - transform.position;
        float distance = toOther.magnitude;
        if (distance < 0.01f) return;

        Vector3 direction = toOther.normalized;

        float interactionForce = (force * otherMagnet.force) / (distance * distance + 0.01f);

        rb.AddForce(direction * interactionForce);

        // Для отрисовки стрелки
        lastForceDirection = direction;
        lastForceMagnitude = interactionForce;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Отрисовка стрелки силы
        Gizmos.color = lastForceMagnitude >= 0 ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + lastForceDirection * Mathf.Sign(lastForceMagnitude) * 1.5f);
    }
}
