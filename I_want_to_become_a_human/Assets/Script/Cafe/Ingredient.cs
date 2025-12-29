using UnityEngine;

[CreateAssetMenu(menuName = "Cafe/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;   // 원두, 우유
    public int expireTime;           // 기본 유통기한 (일)
}
