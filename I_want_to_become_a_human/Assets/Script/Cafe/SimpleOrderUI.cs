using UnityEngine;
using TMPro;

public class SimpleOrderUI : MonoBehaviour
{
    public Transform target; // 손님
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = "아메리카노"; // 테스트용
    }

    void Update()
    {
        if (target == null) return;

        // 👉 월드 위치 → 화면 위치로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(
            target.position + new Vector3(0, 2f, 0)
        );

        transform.position = screenPos;
    }
}