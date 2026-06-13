using UnityEngine;
using BulletPoint;

public class TestBullet : MonoBehaviour
{
    public BulletPoint.BulletPoint bulletList;

    private void Start()
    {
        bulletList.AddBulletPoint("손님 5명 응대");
        bulletList.AddBulletPoint("커피 10잔 판매");
        bulletList.AddBulletPoint("수익 5000원 달성");
    }
}