using UnityEngine;

public class CoffeeTreeHarvest : MonoBehaviour
{
    public Ingredient coffeeBean;
    public IngredientInventory inventory;

    // 클릭 시 채집
    void OnMouseDown()
    {
        Harvest();
    }

    public void Harvest()
    {
        if (coffeeBean != null && inventory != null)
        {
            inventory.AddIngredient(coffeeBean, 1);
            Debug.Log("🌱 원두 채취!");
        }
        else
        {
            Debug.LogWarning("CoffeeBean 또는 Inventory가 연결되지 않음!");
        }
    }

    // 테스트용 키
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Harvest();
        }
    }
}
