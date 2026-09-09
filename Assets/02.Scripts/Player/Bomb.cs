using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("폭탄 이동")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _moveTime = 0.8f;

    [Header("폭탄 유지 시간")]
    [SerializeField] private float _lifeTime = 3f;

    [Header("최대 공격 가능 적 수")]
    [SerializeField] private int _maxHitCount = 10;

    private float _moveTimer = 0f;

    private HashSet<Enemy> _hitEnemies = new HashSet<Enemy>();

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _moveTimer += Time.deltaTime;

        // 설정한 시간 동안만 앞으로 이동
        if (_moveTimer < _moveTime)
        {
            transform.position +=
                Vector3.up * _moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            return;
        }

        // 이미 맞춘 적이면 다시 카운트하지 않음
        if (_hitEnemies.Contains(enemy))
        {
            return;
        }

        _hitEnemies.Add(enemy);

        // 적 즉사
        enemy.TakeDamage(enemy.Health);

        // 10마리 맞추면 폭탄 삭제
        if (_hitEnemies.Count >= _maxHitCount)
        {
            Destroy(gameObject);
        }
    }
}