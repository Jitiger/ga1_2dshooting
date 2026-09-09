using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    [Header("피격 사운드")]
    [SerializeField] private AudioSource _damagedAudioSource;

    [Header("플레이어 죽음 이펙트")]
    [SerializeField] private GameObject _playerDeathEffectPrefab;

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            if (_playerDeathEffectPrefab != null)
            {
                Instantiate(
                    _playerDeathEffectPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
            return;
        }

        if (_damagedAudioSource != null)
        {
            _damagedAudioSource.Play();
        }
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;

        Debug.Log("Player Health : " + _health);
    }
}