using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damage;

    private Animator _animator;
    private bool _isDead = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        if (_isDead)
        {
            return;
        }

        _health -= damage;

        // 피격 애니메이션 실행
        if (_animator != null)
        {
            _animator.SetTrigger("Hit");
        }

        if (_health <= 0)
        {
            _isDead = true;

            ItemDrop itemDrop = GetComponent<ItemDrop>();

            if (itemDrop != null)
            {
                itemDrop.Drop();
            }

            // Hit 애니메이션을 잠깐 보여준 뒤 삭제
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}