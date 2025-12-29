using UnityEngine;

public class CameraRotateHold : MonoBehaviour
{
    public float rotationSpeed = 50f; // 1초에 회전하는 각도

    void Update()
    {
        // 왼쪽 마우스 버튼을 꾹 누르고 있을 때
        if (Input.GetMouseButton(0))
        {
            // y축 기준으로 회전
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
