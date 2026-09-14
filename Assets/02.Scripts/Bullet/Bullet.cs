using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] private float _moveSpeed = 10f;

    [Header("데미지")]
    [SerializeField] private int _damage = 50;

    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
    }


    public void Spawn(Vector3 position)
    {
        // 먼저 위치 지정
        transform.position = position;

        // 총알 활성화
        gameObject.SetActive(true);

        // 발사음
        if (_audioSource != null)
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);

            _audioSource.Play();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy =
            collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }

        // Destroy하지 않고 풀로 반환
        gameObject.SetActive(false);
    }


    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}