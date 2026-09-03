using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        main,
        sub
    }

    public BulletType Type;

    // 목적: 총알을 위로 움직이고 싶다.
    public float MoveSpeed;

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1,0)
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // 충돌 관련 이벤트 (Enter -> Stay -> Exit)
    // 충돌이 시작되면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            //GetComponent<타입>()-> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Health -= 10;

            if (enemy.Health <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}