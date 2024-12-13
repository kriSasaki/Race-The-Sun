using Project.Players.Logic;
using UnityEngine;

public class InfiniteTunnel : MonoBehaviour
{
    private const float TriggerPosition = 1000f;

    [SerializeField] private Player _player; 
    
    public GameObject[] tunnelSegments; // Массив сегментов туннеля
    public float speed = 100f;           // Скорость движения туннеля
    public float segmentLength = 1000f;  // Длина одного сегмента
    
    void Update()
    {
        foreach (GameObject segment in tunnelSegments)
        {
            // Двигаем сегмент назад по Z
            segment.transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);

            // Если сегмент ушел за игрока, перемещаем его вперед
            if (segment.transform.position.z < -TriggerPosition)
            {
                Vector3 newPosition = segment.transform.position;
                newPosition.z += segmentLength * tunnelSegments.Length; // Перемещаем сегмент в начало
                segment.transform.position = newPosition;
            }
        }
    }
}