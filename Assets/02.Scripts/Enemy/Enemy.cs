using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Enemy : MonoBehaviour
{
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

    [Header("피격 사운드")]
    [SerializeField] private AudioClip _damagedSound;

    [SerializeField] private float _damagedVolume = 1f;

    [Header("죽음 이펙트")]
    [SerializeField] private GameObject _enemyDeathEffectPrefab;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Coroutine _hitCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

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

        // 피격 사운드
        PlayDamagedSound();

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

            // 싱글톤 패턴
            // 1. 전역적으로 접근 가능하다.
            // 2. 인스턴스(생성된 객체)가 하나임을 보장한다.
            ScoreManager.Instance.AddScore(100);
            Destroy(gameObject, 0.2f);
        }
    }

    private void PlayDamagedSound()
    {
        if (_damagedSound == null)
        {
            return;
        }

        GameObject soundObject =
            new GameObject("EnemyDamagedSound");

        AudioSource audioSource =
            soundObject.AddComponent<AudioSource>();

        audioSource.clip = _damagedSound;
        audioSource.volume = _damagedVolume;

        // 2D 사운드
        audioSource.spatialBlend = 0f;

        audioSource.Play();

        Destroy(
            soundObject,
            _damagedSound.length
        );
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