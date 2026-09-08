using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 종류")]
    [SerializeField] private ItemType _itemType;

    [Header("아이템 이동")]
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _moveSpeed = 5f;

    [Header("아이템 효과")]
    [SerializeField] private int _healAmount = 20;
    [SerializeField] private float _moveSpeedIncrease = 1f;
    [SerializeField] private float _attackSpeedIncrease = 0.1f;

    [Header("아이템 획득 이펙트")]
    [SerializeField] private GameObject _pickupEffectPrefab;

    private float _timer = 0f;
    private Transform _player;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _waitTime)
        {
            return;
        }

        if (_player == null)
        {
            return;
        }

        Vector2 direction =
            (_player.position - transform.position).normalized;

        transform.position +=
            (Vector3)direction * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ApplyEffect(other);

        // 아이템 획득 이펙트 생성
        if (_pickupEffectPrefab != null)
        {
            Instantiate(
                _pickupEffectPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }

    // 심화 과제 1. 퍼사드 패턴
    // 심화 과제 2. 아이템 종류가 조합에 의해 폭발적으로 증가할 경우에는 조합 패턴 사용
    // 포트폴리오에서 가장 중요한 것은 게임 구현 완성도
    private void ApplyEffect(Collider2D other)
    {
        switch (_itemType)
        {
            case ItemType.FireRateUp:
                IncreaseAttackSpeed(other);
                break;

            case ItemType.Heal:
                HealPlayer(other);
                break;

            case ItemType.MoveSpeed:
                IncreaseMoveSpeed(other);
                break;
        }
    }

    private void IncreaseAttackSpeed(Collider2D other)
    {
        PlayerFire playerFire =
            other.GetComponentInParent<PlayerFire>();

        if (playerFire == null)
        {
            return;
        }

        playerFire.IncreaseAttackSpeed(
            _attackSpeedIncrease
        );
    }

    private void HealPlayer(Collider2D other)
    {
        Player player =
            other.GetComponentInParent<Player>();

        if (player == null)
        {
            return;
        }

        player.Heal(
            _healAmount
        );
    }

    private void IncreaseMoveSpeed(Collider2D other)
    {
        PlayerMove playerMove =
            other.GetComponentInParent<PlayerMove>();

        if (playerMove == null)
        {
            return;
        }

        playerMove.IncreaseMoveSpeed(
            _moveSpeedIncrease
        );
    }
}