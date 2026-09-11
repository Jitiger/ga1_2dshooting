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
        // 1번 키 자동 발사 ON / OFF
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        CoolTimer -= Time.deltaTime;

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
        Bullet rightBullet =
            BulletPool.Instance.GetBullet(
                Bullet.BulletType.Main
            );

        if (rightBullet != null)
        {
            rightBullet.transform.position =
                RightFirePoint.position;
        }

        Bullet leftBullet =
            BulletPool.Instance.GetBullet(
                Bullet.BulletType.Main
            );

        if (leftBullet != null)
        {
            leftBullet.transform.position =
                LeftFirePoint.position;
        }
    }


    // Red Bullet
    public void SubFire()
    {
        Bullet subRightBullet =
            BulletPool.Instance.GetBullet(
                Bullet.BulletType.Sub
            );

        if (subRightBullet != null)
        {
            subRightBullet.transform.position =
                SubRightFirePoint.position;
        }

        Bullet subLeftBullet =
            BulletPool.Instance.GetBullet(
                Bullet.BulletType.Sub
            );

        if (subLeftBullet != null)
        {
            subLeftBullet.transform.position =
                SubLeftFirePoint.position;
        }
    }


    public void IncreaseAttackSpeed(float amount)
    {
        FireRate -= amount;

        if (FireRate < 0.1f)
        {
            FireRate = 0.1f;
        }
    }
}