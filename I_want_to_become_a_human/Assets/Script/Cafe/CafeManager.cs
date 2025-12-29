using UnityEngine;

public class CafeManager : MonoBehaviour
{
    public IngredientInventory inventory;

    public Ingredient coffeeBean;
    public Ingredient milk;

    public int money = 0;      // 플레이어 돈
    public int lattePrice = 3000; // 카페라떼 판매 가격

    // 카페라떼 제작 + 돈 획득
    public void MakeCafeLatte()
    {
        bool hasBean = inventory.UseIngredient(coffeeBean, 1);
        bool hasMilk = inventory.UseIngredient(milk, 1);

        if (hasBean && hasMilk)
        {
            money += lattePrice;
            Debug.Log("☕ 카페라떼 완성! 돈 +" + lattePrice + "원, 총금액: " + money + "원");
        }
        else
        {
            Debug.Log("❌ 재료 부족");
        }
    }
}
