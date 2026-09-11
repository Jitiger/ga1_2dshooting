using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Main,
        Sub
    }

    [Header("총알 종류")]
    [SerializeField] private BulletType _bulletType;

    [Header("이동 속도")]
    [SerializeField] private float _moveSpeed = 10f;

    [Header("데미지")]
    [SerializeField] private int _damage = 50;

    public BulletType Type => _bulletType;
    public int Damage => _damage;

    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Enemy에 TakeDamage가 있다면 사용
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            // Destroy하지 않고 풀로 반환
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 풀로 반환
        gameObject.SetActive(false);
    }
}