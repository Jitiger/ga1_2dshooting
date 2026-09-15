using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _coolTime = 10f;

    private float _coolTimer = 0f;

    private void Update()
    {
        if (_coolTimer > 0)
        {
            _coolTimer -= Time.deltaTime;
        }

        if (SimpleInput.GetButton("Bomb") && _coolTimer <= 0)
        {
            UseBomb();
            _coolTimer = _coolTime;
        }
    }

    private void UseBomb()
    {
        Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    }
}