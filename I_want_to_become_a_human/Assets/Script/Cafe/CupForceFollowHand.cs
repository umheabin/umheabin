using UnityEngine;

public class CupForceFollowHand : MonoBehaviour
{
    [Header("오른손 컵 그립 포인트")]
    public Transform cupGripPointR;

    [Header("컵 회전 보정")]
    public Vector3 rotationOffset;

    [Header("따라가기")]
    public bool follow = true;

    [Header("부드러운 추적 (0 = 즉시)")]
    public float followSpeed = 0f;

    // ⭐ 컵 기울기용 추가 회전 (DrinkTilt에서 제어)
    [HideInInspector]
    public Quaternion extraLocalRotation = Quaternion.identity;

    void LateUpdate()
    {
        if (!follow || cupGripPointR == null) return;

        // ===== 위치 따라가기 =====
        if (followSpeed <= 0f)
        {
            transform.position = cupGripPointR.position;
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                cupGripPointR.position,
                Time.deltaTime * followSpeed
            );
        }

        // ===== 손 회전 =====
        float handY = cupGripPointR.eulerAngles.y;

        Quaternion handRotation = Quaternion.Euler(
            rotationOffset.x,
            handY + rotationOffset.y,
            rotationOffset.z
        );

        // ⭐ 손 회전 + 컵 기울기 회전 합치기
        transform.rotation = handRotation * extraLocalRotation;
    }

    public void PickUpCup(Transform gripPoint)
    {
        cupGripPointR = gripPoint;
        follow = true;
        extraLocalRotation = Quaternion.identity;
    }

    public void PutDownCup()
    {
        follow = false;
        cupGripPointR = null;
        extraLocalRotation = Quaternion.identity;
    }
}
