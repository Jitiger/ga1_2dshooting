using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;

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
        transform.Translate((direction * speed * Time.deltaTime));
    }
}