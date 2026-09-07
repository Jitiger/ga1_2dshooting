using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    // 필요 필드:
    [Header("플레이어 이동 속도")]
    [SerializeField] private float _speed;

    [Header("플레이어 이동 가능 좌표")]
    [SerializeField] private float _maxPositionY;
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionX;
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        // 3. 방향과 속력에 따라 이동한다.
        Vector2 newPosition = transform.position + (Vector3)normalizedDirection * _speed * Time.deltaTime;

        // 4. 위치 y에 제한이 있다.
        if (newPosition.y > _maxPositionY)
        {
            newPosition.y = _maxPositionY;
        }
        else if (newPosition.y < _minPositionY)
        {
            newPosition.y = _minPositionY;
        }

        // 5. 양 옆 끝으로 가면 반대쪽 방향으로 이동
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
        Debug.Log("Move Speed : " + _speed);
    }
}