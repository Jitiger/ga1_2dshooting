using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;

    [Header("플레이어 이동 속도")]
    [SerializeField] private float _speed = 5f;

    public float Speed => _speed;

    [Header("플레이어 이동 가능 좌표")]
    [SerializeField] private float _maxPositionY;
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionX;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        float h = SimpleInput.GetAxisRaw("Horizontal");

        float v = SimpleInput.GetAxisRaw("Vertical");

        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        _animator.SetInteger("x", (int)h);

        float finalSpeed = _speed + UpgradeManager.Instance.Upgrades[2].CurrentValue;

        Vector2 newPosition = transform.position + (Vector3)normalizedDirection * finalSpeed * Time.deltaTime;

        if (newPosition.y > _maxPositionY)
        {
            newPosition.y = _maxPositionY;
        }
        else if (newPosition.y < _minPositionY)
        {
            newPosition.y = _minPositionY;
        }

        if (newPosition.x > _maxPositionX)
        {
            newPosition.x = _minPositionX;
        }
        else if (newPosition.x < _minPositionX)
        {
            newPosition.x = _maxPositionX;
        }

        transform.position = newPosition;
    }


    public void IncreaseMoveSpeed(float amount)
    {
        _speed += amount;
    }
}