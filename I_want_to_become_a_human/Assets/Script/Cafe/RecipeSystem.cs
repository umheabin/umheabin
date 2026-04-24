using UnityEngine;
using System.Collections.Generic;

public class RecipeSystem : MonoBehaviour
{
    public static RecipeSystem Instance;

    // 👉 정답 레시피 (재료 : 필요한 개수)
    private Dictionary<string, int> recipe;

    // 👉 플레이어 입력 기록 (재료 : 넣은 개수)
    private Dictionary<string, int> currentInput;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 👉 주문에 따라 레시피 초기화
    /// </summary>
    public void StartRecipe(string order)
    {
        recipe = new Dictionary<string, int>();
        currentInput = new Dictionary<string, int>();

        switch (order)
        {
            case "아메리카노":
                recipe["원두"] = 1;
                recipe["뜨거운물"] = 1;
                break;

            case "라떼":
                recipe["원두"] = 1;
                recipe["우유"] = 1;
                break;

            case "케이크":
                recipe["빵"] = 1;
                recipe["생크림"] = 1;
                recipe["딸기"] = 1;
                break;

            case "쿠키":
                recipe["박력분"] = 1;
                recipe["버터"] = 1;
                recipe["설탕"] = 1;
                recipe["달걀"] = 1;
                break;
        }

        Debug.Log("레시피 시작: " + order);
    }

    /// <summary>
    /// 👉 재료 입력 (순서 상관 없음)
    /// </summary>
    public void AddIngredient(string ingredient)
    {
        if (!GameManager.Instance.IsCooking()) return;

        // ❌ 레시피에 없는 재료 → 실패
        if (!recipe.ContainsKey(ingredient))
        {
            Debug.Log("잘못된 재료: " + ingredient);
            GameManager.Instance.FailOrder();
            return;
        }

        // ⭐ 중요: 없으면 먼저 초기화
        if (!currentInput.ContainsKey(ingredient))
        {
            currentInput[ingredient] = 0;
        }

        // 👉 개수 증가
        currentInput[ingredient]++;

        // ❌ 초과하면 실패
        if (currentInput[ingredient] > recipe[ingredient])
        {
            Debug.Log("재료 초과: " + ingredient);
            GameManager.Instance.FailOrder();
            return;
        }

        Debug.Log("입력: " + ingredient);

        // ⭐ 완료 체크 (모든 재료 충족 여부)
        foreach (var item in recipe)
        {
            string key = item.Key;
            int required = item.Value;

            if (!currentInput.ContainsKey(key) ||
                currentInput[key] < required)
            {
                return; // 아직 덜 넣음
            }
        }

        // 👉 여기까지 오면 100% 성공
        Debug.Log("요리 완성!");
        GameManager.Instance.CompleteOrder();
    }
}