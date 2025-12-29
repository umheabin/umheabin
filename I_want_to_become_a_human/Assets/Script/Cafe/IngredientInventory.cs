using System.Collections.Generic;
using UnityEngine;

public class IngredientInventory : MonoBehaviour


{
    public List<IngredientGroup> ingredients = new List<IngredientGroup>();

    // 재료 추가
    public void AddIngredient(Ingredient ingredient, float amount)
    {
        ingredients.Add(new IngredientGroup(ingredient, amount));
        Debug.Log($"{ingredient.ingredientName} {amount}개 추가됨");
    }

    // 재료 사용
    public bool UseIngredient(Ingredient ingredient, float amount)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredient == ingredient)
            {
                amount = ingredients[i].ReduceQuantity(amount);

                if (ingredients[i].quantity <= 0)
                {
                    ingredients.RemoveAt(i);
                }

                if (amount <= 0)
                    return true;
            }
        }
        return false;
    }
}
