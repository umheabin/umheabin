using UnityEngine;

[System.Serializable]
public class IngredientGroup
{
    public Ingredient ingredient; // 어떤 재료
    public float quantity;         // 수량
    public int expireTime;          // 남은 유통기한

    public IngredientGroup(Ingredient ingredient, float quantity)
    {
        this.ingredient = ingredient;
        this.quantity = quantity;
        this.expireTime = ingredient.expireTime;
    }

    // 재료 소모
    public float ReduceQuantity(float amount)
    {
        if (quantity >= amount)
        {
            quantity -= amount;
            return 0f;
        }
        else
        {
            float remain = amount - quantity;
            quantity = 0f;
            return remain;
        }
    }
}
