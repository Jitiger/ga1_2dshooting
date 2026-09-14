using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    [Header("피격 사운드")]
    [SerializeField] private AudioClip _damagedSound;
    [SerializeField] private float _damagedVolume = 1f;

    [Header("플레이어 죽음 이펙트")]
    [SerializeField] private GameObject _playerDeathEffectPrefab;

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        PlayDamagedSound();

        if (_health <= 0)
        {
            Die();
        }
    }

    private void PlayDamagedSound()
    {
        if (_damagedSound == null)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(
            _damagedSound,
            transform.position,
            _damagedVolume
        );
    }

    private void Die()
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
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;

        Debug.Log("Player Health : " + _health);
    }
}