using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 👉 재료 버튼 패널
    public GameObject ingredientPanel;

    private string currentOrder;
    private float timer;
    private bool isCooking = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 👉 시작 시 버튼 숨김
        if (ingredientPanel != null)
            ingredientPanel.SetActive(false);
    }

    void Update()
    {
        // 👉 요리 중이 아닐 때는 아무것도 안 함
        if (!isCooking) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            FailOrder();
        }
    }

    /// <summary>
    /// 👉 손님 클릭 → 요리 시작
    /// </summary>
    public void StartCooking(string order)
    {
        currentOrder = order;

        timer = 8f;
        isCooking = true;

        if (ingredientPanel != null)
            ingredientPanel.SetActive(true);

        // 👉 레시피 시작
        RecipeSystem.Instance.StartRecipe(order);

        Debug.Log("요리 시작: " + order);
    }

    /// <summary>
    /// 👉 성공 처리
    /// </summary>
    public void CompleteOrder()
    {
        // ⭐ 이미 끝났으면 무시 (중복 방지 핵심)
        if (!isCooking) return;

        // ⭐ 먼저 상태 잠금
        isCooking = false;
        timer = 0f;

        Debug.Log("성공 💰");

        if (ingredientPanel != null)
            ingredientPanel.SetActive(false);
    }

    /// <summary>
    /// 👉 실패 처리
    /// </summary>
    public void FailOrder()
    {
        // ⭐ 이미 끝났으면 무시 (중복 방지 핵심)
        if (!isCooking) return;

        // ⭐ 먼저 상태 잠금
        isCooking = false;
        timer = 0f;

        Debug.Log("실패 😡");

        if (ingredientPanel != null)
            ingredientPanel.SetActive(false);
    }

    /// <summary>
    /// 👉 현재 요리 상태 확인
    /// </summary>
    public bool IsCooking()
    {
        return isCooking;
    }
}