using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Main Bullet - Green")]
    [SerializeField] private Bullet _greenBulletPrefab;

    [Header("Sub Bullet - Red")]
    [SerializeField] private Bullet _redBulletPrefab;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize = 30;

    // Green Bullet 전용 풀
    private Bullet[] _greenPool;

    // Red Bullet 전용 풀
    private Bullet[] _redPool;


    // 싱글톤
    private static BulletPool _instance;

    public static BulletPool Instance => _instance;


    private void Awake()
    {
        // 이미 BulletPool이 존재한다면 중복 생성 방지
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // Green / Red 풀 생성
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
        // Main이면 Green Pool 사용
        if (type == Bullet.BulletType.Main)
        {
            return GetBulletFromPool(_greenPool);
        }

        // Sub이면 Red Pool 사용
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
            // 사용중이지 않은 총알 발견
            if (bullet.gameObject.activeSelf == false)
            {
                // 다시 활성화
                bullet.gameObject.SetActive(true);

                return bullet;
            }
        }

        // 사용할 수 있는 총알이 없음
        Debug.LogWarning("Bullet Pool에 사용할 수 있는 총알이 없습니다.");

        return null;
    }
}