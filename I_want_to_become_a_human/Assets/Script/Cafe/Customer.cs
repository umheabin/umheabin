using UnityEngine;

public class Customer : MonoBehaviour
{
    // 👉 이 손님의 주문
    public string order;

    void Start()
    {
        GenerateOrder();
    }

    /// <summary>
    /// 👉 랜덤 메뉴 생성
    /// </summary>
    void GenerateOrder()
    {
        string[] menu = { "아메리카노", "라떼", "케이크", "쿠키" };

        int index = Random.Range(0, menu.Length);
        order = menu[index];

       
    }

    /// <summary>
    /// 👉 손님 클릭 → 요리 시작
    /// </summary>
    void OnMouseDown()
    {
        GameManager.Instance.StartCooking(order);
    }
}