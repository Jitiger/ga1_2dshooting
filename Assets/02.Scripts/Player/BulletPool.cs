using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("총알 프리팹")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("총알 종류별 풀 사이즈")]
    [SerializeField] private int _poolSize = 30;


    private Bullet[][] _pools;


    private static BulletPool _instance;

    public static BulletPool Instance => _instance;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 총알 종류 수만큼 풀 생성
        _pools = new Bullet[_bulletPrefabs.Length][];

        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            // i번째 총알 전용 풀 생성
            _pools[i] = new Bullet[_poolSize];

            // 총알 미리 생성
            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = Instantiate(_bulletPrefabs[i], transform);

                bullet.gameObject.SetActive(false);

                _pools[i][j] = bullet;
            }
        }
    }


    public Bullet GetBullet(
        BulletType type,
        Vector3 position
    )
    {
        int index = (int)type;

        Bullet[] pool = _pools[index];

        foreach (Bullet bullet in pool)
        {
            if (bullet == null)
            {
                continue;
            }

            if (bullet.gameObject.activeSelf == false)
            {
                bullet.Spawn(position);

                return bullet;
            }
        }

        Debug.LogWarning(
            $"{type} 총알 Pool이 부족합니다."
        );

        return null;
    }
}