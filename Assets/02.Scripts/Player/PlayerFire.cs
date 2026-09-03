using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject GreenBullet;

    // - 생성 위치(총구)
    public Transform RightFirePoint;
    public Transform LeftFirePoint;

    public GameObject RedBullet;
    public Transform SubRightFirePoint;

    public Transform SubLeftFirePoint;

    // - 쿨타이머
    public float CoolTime = 0.5f;
    public float CoolTimer = 0;

    // - Auto 모드
    public bool AutoFireMode = false;

    private void Start()
    {
        CoolTimer = CoolTime;
    }

    // public Transform[] FirePoint;

    private void Update()
    {
        // 오토 공격 모드 토글
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        CoolTimer -= Time.deltaTime;

        // 1. 쿨타이머가 0초 이하이고 && (스페이스바를 누르거나 || 오토 모드라면)
        if (CoolTimer <= 0 && (Input.GetKeyDown(KeyCode.Space) || AutoFireMode))
        {
            // 2. 발사


            Fire();
            SubFire();
            // 3. 쿨타이머 초기화
            CoolTimer = CoolTime;
        }
    }

    public void SubFire()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject SubRightbullet = Instantiate(RedBullet);
            SubRightbullet.transform.position = SubRightFirePoint.position;

            GameObject SubLeftbullet = Instantiate((RedBullet));
            SubLeftbullet.transform.position = SubLeftFirePoint.position;
        }
    }

    public void Fire()
    {
        // 1. 스페이스바를 누르면
        // if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 (Monobehaviour를 상속받는) 게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject Rightbullet = Instantiate((GreenBullet));
            Rightbullet.transform.position = RightFirePoint.position; // 생성한 총알의 위치를 나(플레이어)의 위치로!!

            GameObject Leftbullet = Instantiate((GreenBullet));
            Leftbullet.transform.position = LeftFirePoint.position;
        }
    }
}