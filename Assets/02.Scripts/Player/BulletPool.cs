using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Main Bullet - Green")]
    [SerializeField] private Bullet _greenBulletPrefab;

    [Header("Sub Bullet - Red")]
    [SerializeField] private Bullet _redBulletPrefab;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize = 30;


    private Bullet[] _greenPool;
    private Bullet[] _redPool;


    private static BulletPool _instance;

    public static BulletPool Instance => _instance;


    private void Awake()
    {
        // BulletPool 중복 생성 방지
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // Green / Red 총알 저장 공간 생성
        _greenPool = new Bullet[_poolSize];
        _redPool = new Bullet[_poolSize];

        // Green Bullet 미리 생성
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(
                _greenBulletPrefab,
                transform
            );

            bullet.gameObject.SetActive(false);

            _greenPool[i] = bullet;
        }

        // Red Bullet 미리 생성
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(
                _redBulletPrefab,
                transform
            );

            bullet.gameObject.SetActive(false);

            _redPool[i] = bullet;
        }
    }


    public Bullet GetBullet(Bullet.BulletType type)
    {
        if (type == Bullet.BulletType.Main)
        {
            return GetBulletFromPool(_greenPool);
        }

        if (type == Bullet.BulletType.Sub)
        {
            return GetBulletFromPool(_redPool);
        }

        return null;
    }


    private Bullet GetBulletFromPool(Bullet[] pool)
    {
        foreach (Bullet bullet in pool)
        {
            // 혹시 Destroy된 총알이 있다면 건너뛰기
            if (bullet == null)
            {
                continue;
            }

            // 현재 사용하지 않는 총알 찾기
            if (bullet.gameObject.activeSelf == false)
            {
                bullet.gameObject.SetActive(true);

                return bullet;
            }
        }

        Debug.LogWarning(
            "Bullet Pool에 사용할 수 있는 총알이 없습니다."
        );

        return null;
    }
}