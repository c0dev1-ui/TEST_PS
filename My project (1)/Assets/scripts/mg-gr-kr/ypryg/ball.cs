using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float elasticity = 0.8f;
    public float gravity = -9.81f;
    public float initialVelocity = 5f;
    public float groundY = 0f;

    private float velocityY = 0f;
    private bool bouncing = false;

    private void Start()
    {
        elasticity = PlayerPrefs.GetFloat("BallElasticity", 0.8f);
        StartBouncing();
    }

    public void StartBouncing()
    {
        velocityY = initialVelocity;
        bouncing = true;
    }

    private void Update()
    {
        if (!bouncing) return;

        // Обновляем скорость с учетом гравитации
        velocityY += gravity * Time.deltaTime;

        // Обновляем позицию
        Vector3 pos = transform.position;
        pos.y += velocityY * Time.deltaTime;

        // Проверяем столкновение с полом
        if (pos.y <= groundY)
        {
            pos.y = groundY;

            // Инвертируем скорость с упругостью
            velocityY = -velocityY * elasticity;

            // Если скорость слишком мала, останавливаем мяч
            if (Mathf.Abs(velocityY) < 0.1f)
            {
                velocityY = 0f;
                bouncing = false;
            }
        }

        transform.position = pos;
    }
}
