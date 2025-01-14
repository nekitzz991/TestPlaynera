using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 5f; // Скорость скроллинга

    // Границы камеры
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private void Update()
    {
        // Получение ввода от игрока
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Вычисление направления движения
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        // Обновление позиции камеры
        transform.position += direction * scrollSpeed * Time.deltaTime;

        // Ограничение позиции камеры в пределах границ
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
