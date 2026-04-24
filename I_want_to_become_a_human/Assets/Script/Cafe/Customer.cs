using UnityEngine;

public class Customer : MonoBehaviour
{
    // 👉 생성할 주문 UI 프리팹 (Canvas + Text 포함)
    public GameObject orderUIPrefab;

    // 👉 현재 손님의 주문 (나중에 메뉴 데이터로 확장 가능)
    private string order;

    void Start()
    {
        // 1️⃣ 손님 주문 생성
        GenerateOrder();

        // 2️⃣ 주문 UI 생성
        CreateOrderUI();
    }

    /// <summary>
    /// 👉 랜덤 주문 생성 (테스트용)
    /// 나중에는 메뉴 데이터 연결하면 됨
    /// </summary>
    void GenerateOrder()
    {
        string[] menu = { "아메리카노", "라떼", "케이크", "쿠키" };

        // 랜덤으로 하나 선택
        int index = Random.Range(0, menu.Length);
        order = menu[index];
    }

    /// <summary>
    /// 👉 주문 UI 생성 + 손님 머리 위에 붙이기
    /// </summary>
    void CreateOrderUI()
    {
        // 1️⃣ 프리팹 복제
        GameObject ui = Instantiate(orderUIPrefab);

        // 2️⃣ OrderUI 스크립트 가져오기
        OrderUI orderUI = ui.GetComponent<OrderUI>();

        // 3️⃣ 손님 위치 + 주문 텍스트 전달
        orderUI.Init(transform, order);
    }
}