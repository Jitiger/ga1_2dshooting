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
    [SerializeField] private float _fireRate = 0.5f;

    private float _coolTimer;
    private bool _isAutoFireMode;

    public float FireRate => _fireRate;


    private void Start()
    {
        _coolTimer = _fireRate;
    }


    private void Update()
    {
        _coolTimer -= Time.deltaTime;

        bool manualFireInput =
            Input.GetKeyDown(KeyCode.Space);

        if (_coolTimer <= 0f &&
            (manualFireInput || _isAutoFireMode))
        {
            Fire();
            SubFire();

            float finalFireRate =
                _fireRate -
                UpgradeManager.Instance
                    .Upgrades[1]
                    .CurrentValue;

            if (finalFireRate < 0.1f)
            {
                finalFireRate = 0.1f;
            }

            _coolTimer = finalFireRate;
        }
    }


    public void SetAuto(bool isAutoMode)
    {
        _isAutoFireMode = isAutoMode;
    }


    public void Fire()
    {
        BulletPool.Instance.GetBullet(
            BulletType.Main,
            RightFirePoint.position
        );

        BulletPool.Instance.GetBullet(
            BulletType.Main,
            LeftFirePoint.position
        );
    }


    public void SubFire()
    {
        BulletPool.Instance.GetBullet(
            BulletType.Sub,
            SubRightFirePoint.position
        );

        BulletPool.Instance.GetBullet(
            BulletType.Sub,
            SubLeftFirePoint.position
        );
    }


    public void IncreaseAttackSpeed(float amount)
    {
        _fireRate -= amount;

        if (_fireRate < 0.1f)
        {
            _fireRate = 0.1f;
        }
    }
}