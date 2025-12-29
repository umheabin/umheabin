using UnityEngine;

public class FixedOrbitCamera : MonoBehaviour
{
    public Transform target;        // 플레이어 캐릭터
    public float distance = 5f;     // 카메라와 캐릭터 사이 거리
    public float height = 2f;       // 카메라 높이
    public float rotationSpeed = 5f; // 마우스 회전 속도

    private float currentX = 0f;    // 좌우 회전 각도
    private float currentY = 10f;   // 상하 회전 각도

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스 입력으로 회전
       

        // 회전 적용
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);

        // 고정된 위치에서 카메라 회전
        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
