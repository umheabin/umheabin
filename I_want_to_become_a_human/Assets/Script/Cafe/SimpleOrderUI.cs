using UnityEngine;
using TMPro; // 👉 TextMeshPro 사용

public class SimpleOrderUI : MonoBehaviour
{
    // 👉 이 UI가 따라다닐 대상 (손님)
    // Inspector에서 연결
    public Transform target;

    // 👉 화면에 글자를 표시하는 Text 컴포넌트
    // Inspector에서 자기 자신(OrderText) 연결
    public TextMeshProUGUI text;

    // 👉 target에 붙어있는 Customer 스크립트 저장용
    private Customer customer;

    // 👉 시작 시 한 번 실행
    void Start()
    {
        // 👉 target(손님)에서 Customer 스크립트 가져오기
        customer = target.GetComponent<Customer>();

        // 👉 Customer가 존재하면
        if (customer != null)
        {
            // 👉 손님의 주문을 가져와 텍스트에 표시
            text.text = customer.order;
        }
        else
        {
            // 👉 Customer 없으면 에러 로그 출력
            Debug.Log("Customer 스크립트 없음!");
        }
    }

    void Update()
    {
        // 👉 target 없으면 실행 중지
        if (target == null) return;

        // 👉 손님 머리 위 위치 계산 (Y + 2)
        Vector3 worldPos = target.position + new Vector3(0, 2f, 0);

        // 👉 3D 위치 → 화면 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 👉 UI 위치 이동
        transform.position = screenPos;
    }
}