using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    // 👉 이 버튼의 재료 이름
    public string ingredientName;

    /// <summary>
    /// 👉 버튼 클릭 시 실행
    /// </summary>
    public void OnClick()
    {
        RecipeSystem.Instance.AddIngredient(ingredientName);
    }
}