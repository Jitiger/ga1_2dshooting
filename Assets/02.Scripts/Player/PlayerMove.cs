using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
 
   // 필요 필드:
    public float Speed;
    public float MaxPositionY;
    public float MinPositionY;
    public float MaxPositionX;
    public float MinPositionX;


    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical");   // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f

        // 2. 키보드 입력에 따라 방향을 구한다.
        // 게임에는 벡터라는 타입이 있다. 벡터는(크기와 방향을 의미한다)
        Vector2 direction = new Vector2(h, v); // 왼쪽 방향
        // = Vector2 direction = Vector2.left;
        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        // 3. 방향과 속력에 따라 이동한다.
        // 속도 = 방향 * 속력                        // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는
        // 헷갈리는 숫자
        // 0.06
        Vector2 normalizedSpeed = (direction * Speed).normalized; // 벡터의 길이를 1로 만들어주는것 (즉, 방향만 유지한다.)
        transform.Translate(normalizedSpeed * Time.deltaTime);
        // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환
        Vector2 newPosition = transform.position + (Vector3)normalizedDirection * Speed * Time.deltaTime;

        // 4. 위치 y에 제한이 있다.
        if (newPosition.y > MaxPositionY)
        {
            newPosition.y = MaxPositionY;
        }
        else if (newPosition.y < MinPositionY)
        {
            newPosition.y = MinPositionY;
        }
        
        // 5. 양 옆 끝으로 가면 반대쪽 방향으로 이동
        if (newPosition.x > MaxPositionX)
        {
            newPosition.x = MinPositionX;
        }
        else if (newPosition.x < MinPositionX)
        {
            newPosition.x = MaxPositionX;
        }

        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        // transform.position += (Vector3)direction * Speed * Time.deltaTime;
        transform.position = newPosition;


        // 7. Q/E 버튼 입력을 통한 스피드 업/다운
        SpeedChange();
        
    }

    private void SpeedChange()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed--;
        }
    }
}