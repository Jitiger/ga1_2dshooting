using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Main Fire Point")]
    public Transform RightFirePoint;
    public Transform LeftFirePoint;


    [Header("Sub Fire Point")]
    public Transform SubRightFirePoint;
    public Transform SubLeftFirePoint;


    [Header("Fire Setting")]
    [field: SerializeField]
    public float FireRate { get; private set; } = 0.5f;


    public float CoolTimer = 0f;

    public bool AutoFireMode = false;


    private void Start()
    {
        CoolTimer = FireRate;
    }


    private void Update()
    {
        // 1번 키로 자동발사 ON / OFF
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        // 쿨타임 감소
        CoolTimer -= Time.deltaTime;

        // 발사 가능 상태
        if (CoolTimer <= 0 &&
            (Input.GetKeyDown(KeyCode.Space) || AutoFireMode))
        {
            Fire();

            SubFire();

            CoolTimer = FireRate;
        }
    }


    // Green Bullet
    public void Fire()
    {
        // 오른쪽 Main 총알
        Bullet rightBullet =
            BulletPool.Instance.GetBullet(Bullet.BulletType.Main);

        if (rightBullet != null)
        {
            rightBullet.transform.position =
                RightFirePoint.position;
        }

        // 왼쪽 Main 총알
        Bullet leftBullet =
            BulletPool.Instance.GetBullet(Bullet.BulletType.Main);

        if (leftBullet != null)
        {
            leftBullet.transform.position =
                LeftFirePoint.position;
        }
    }


    // Red Bullet
    public void SubFire()
    {
        // 오른쪽 Sub 총알
        Bullet subRightBullet =
            BulletPool.Instance.GetBullet(Bullet.BulletType.Sub);

        if (subRightBullet != null)
        {
            subRightBullet.transform.position =
                SubRightFirePoint.position;
        }

        // 왼쪽 Sub 총알
        Bullet subLeftBullet =
            BulletPool.Instance.GetBullet(Bullet.BulletType.Sub);

        if (subLeftBullet != null)
        {
            subLeftBullet.transform.position =
                SubLeftFirePoint.position;
        }
    }


    public void IncreaseAttackSpeed(float amount)
    {
        FireRate -= amount;

        // 최소 공격속도 제한
        if (FireRate < 0.1f)
        {
            FireRate = 0.1f;
        }
    }
}