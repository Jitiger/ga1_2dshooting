using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float Health = 100;

    private void Start()
    {
    }

    private void Update()
    {
        move();
    }

    private void move()
    {
        Vector2 direction = Vector2.down;
        transform.Translate((direction * MoveSpeed * Time.deltaTime));
    }
}