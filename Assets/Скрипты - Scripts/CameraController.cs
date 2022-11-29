using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetToFollow; //Цель, за которой следовать
    public Vector2 offset; //Сдвиг относительно цели

    void Update()
    {
        float x = targetToFollow.position.x + offset.x;
        float y = targetToFollow.position.y + offset.y;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }
}
