using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TextMeshProUGUI orderText;
    private Transform target; // 따라갈 손님

    // 초기 설정
    public void Init(Transform targetTransform, string order)
    {
        target = targetTransform;
        orderText.text = order;
    }

    void Update()
    {
        if (target == null) return;

        // 손님 머리 위 위치
        Vector3 pos = target.position + new Vector3(0, 2f, 0);
        transform.position = pos;

        // 카메라 바라보기
        transform.forward = Camera.main.transform.forward;
    }
}