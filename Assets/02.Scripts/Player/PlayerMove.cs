using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    private Animator _animator;

    [Header("플레이어 이동 속도")]
    [field: SerializeField]
    public float Speed { get; private set; }

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
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 이동 방향을 구한다.
        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        // 좌우 애니메이션
        _animator.SetInteger("x", (int)h);

        // 3. 방향과 속력에 따라 이동한다.
        Vector2 newPosition =
            transform.position +
            (Vector3)normalizedDirection * Speed * Time.deltaTime;

        // 4. Y 위치 제한
        if (newPosition.y > _maxPositionY)
        {
            newPosition.y = _maxPositionY;
        }
        else if (newPosition.y < _minPositionY)
        {
            newPosition.y = _minPositionY;
        }

        // 5. 좌우 끝으로 가면 반대편으로 이동
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
        Speed += amount;
    }
}