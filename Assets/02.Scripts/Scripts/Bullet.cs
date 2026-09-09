using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioSource _audioSource;
    public int Damage;

    // 총알 이동 속도
    public float MoveSpeed;

    private void Update()
    {
        Vector2 direction = Vector2.up;

        transform.Translate(
            direction * MoveSpeed * Time.deltaTime
        );
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = UnityEngine.Random.Range(-1f, 1.5f);
        _audioSource.Play();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}