using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Todo; 에너미가 피격당할때마다 플레이
    private AudioSource _damagedAudioSource;
    [Header("적 체력")]
    [field: SerializeField]
    public int Health { get; private set; } = 100;

    public bool IsDead => Health <= 0;

    [Header("적 능력치")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damage;

    [Header("피격 효과")]
    [SerializeField] private Color _hitColor = new Color(0.6f, 0.3f, 0.3f, 1f);
    [SerializeField] private float _hitDuration = 0.1f;

    [Header("죽음 이펙트")]
    [SerializeField] private GameObject _enemyDeathEffectPrefab;

    private SpriteRenderer _spriteRenderer;

    private Color _originalColor;

    private Coroutine _hitCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_audioSource = GetComponent<AudioSource>();

        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        Health -= damage;

        // 피격 시 현재 애니메이션은 유지하고 색만 변경
        if (_spriteRenderer != null)
        {
            if (_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
            }

            _hitCoroutine = StartCoroutine(HitEffect());
        }

        if (IsDead)
        {
            ItemDrop itemDrop = GetComponent<ItemDrop>();

            if (itemDrop != null)
            {
                itemDrop.Drop();
            }

            if (_enemyDeathEffectPrefab != null)
            {
                Instantiate(
                    _enemyDeathEffectPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }
            Destroy(gameObject, 0.2f);
        }
    }

    private IEnumerator HitEffect()
    {
        _spriteRenderer.color = _hitColor;

        yield return new WaitForSeconds(_hitDuration);

        _spriteRenderer.color = _originalColor;

        _hitCoroutine = null;
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