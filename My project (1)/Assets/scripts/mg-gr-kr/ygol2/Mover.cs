using UnityEngine;

public class PlasmaParticle : MonoBehaviour
{
    public Transform center; // точка притяжения (установить из менеджера)
    public PlasmaParticle[] particles; // все частицы (установить из менеджера)
    private Rigidbody rb;

    public float attractionForce = 10f;
    public float repulsionForce = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (center != null)
        {
            Vector3 toCenter = center.position - transform.position;
            rb.AddForce(toCenter.normalized * attractionForce);
        }

        if (particles != null)
        {
            foreach (var other in particles)
            {
                if (other == this) continue;

                Vector3 away = transform.position - other.transform.position;
                float dist = away.magnitude;
                if (dist > 0.01f) // чтобы не делить на 0
                {
                    rb.AddForce(away.normalized * (repulsionForce / dist));
                }
            }
        }
    }
}
