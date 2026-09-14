using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 발사하고 싶다.
    [Header("Main Fire Point")]
    public Transform RightFirePoint;
    public Transform LeftFirePoint;

    [Header("Sub Fire Point")]
    public Transform SubRightFirePoint;
    public Transform SubLeftFirePoint;

    [Header("Fire Setting")]
    [SerializeField] private float _fireRate = 0.5f;

    private const float MinCoolTime = 0.1f;
    private float _coolTimer;
    private bool _autoFireMode = false;

    public float FireRate => _fireRate;

    private void Start()
    {
        _coolTimer = _fireRate;
    }

    private void Update()
    {
        _coolTimer -= Time.deltaTime;

        if (_coolTimer <= 0f &&
            (Input.GetKeyDown(KeyCode.Space) || _autoFireMode))
        {
            Fire();
            SubFire();

            _coolTimer = _fireRate;
        }
    }

    public void SetAuto(bool auto)
    {
        _autoFireMode = auto;
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

    public void FireRateUp(float upValue)
    {
        if (upValue < 0)
        {
            Debug.LogWarning("공격 속도 증가량은 0보다 작을 수 없습니다.");
            return;
        }

        _fireRate -= upValue;

        if (_fireRate < MinCoolTime)
        {
            _fireRate = MinCoolTime;
        }
    }
}
