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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        CoolTimer -= Time.deltaTime;

        if (CoolTimer <= 0 &&
            (Input.GetKeyDown(KeyCode.Space)
             || AutoFireMode))
        {
            Fire();
            SubFire();

            CoolTimer = FireRate;
        }
    }


    public void Fire()
    {
        // Main 오른쪽
        BulletPool.Instance.GetBullet(
            BulletType.Main,
            RightFirePoint.position
        );

        // Main 왼쪽
        BulletPool.Instance.GetBullet(
            BulletType.Main,
            LeftFirePoint.position
        );
    }


    public void SubFire()
    {
        // Sub 오른쪽
        BulletPool.Instance.GetBullet(
            BulletType.Sub,
            SubRightFirePoint.position
        );

        // Sub 왼쪽
        BulletPool.Instance.GetBullet(
            BulletType.Sub,
            SubLeftFirePoint.position
        );
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