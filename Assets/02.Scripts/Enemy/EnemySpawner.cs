using UnityEngine;

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    [Header("스폰 적 프리팹")]
    [SerializeField] private Enemy _downwardEnemyPrefab;
    [SerializeField] private Enemy _aimedEnemyPrefab;
    [SerializeField] private Enemy _homingEnemyPrefab;

    [Header("스폰 간격")]
    [SerializeField] private float _spawnInterval = 3f;

    private float _timer = 0f;

    [Header("스폰 위치")]
    [SerializeField] private float _spawnMaxPositionX = 0f;
    [SerializeField] private float _spawnMinPositionX = 0f;
    [SerializeField] private float _spawnMaxPositionY = 0f;
    [SerializeField] private float _spawnMinPositionY = 0f;

    [Header("생성할 적의 수")]
    [SerializeField] private int _enemyCount;

    // Todo: Scriptable Object를 사용해서 리팩토링
    // 이유 1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
    // 이유 2: 각 에너미 스폰 확률을 매직넘버로 하드코딩해서 유지보수가 어렵
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            _spawnInterval = Random.Range(1f, 3f);

            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            // 일단 랜덤하게 생성할 x좌표 위치 뽑고
            float randomX = Random.Range(
                _spawnMinPositionX,
                _spawnMaxPositionX
            );
            float randomY = Random.Range(
                _spawnMinPositionY,
                _spawnMaxPositionY
            );

            Vector2 spawnPosition = new Vector2(
                randomX,
                randomY
            );

            // 인스펙터에서 지정한 y좌표 사이에서 위치 뽑기
            int randomValue = Random.Range(0, 100);

            Enemy enemyPrefab;

            //weight 공부한거!!
            // Downward 50%
            if (randomValue < 50)
            {
                enemyPrefab = _downwardEnemyPrefab;
            }
            // Aimed 30%
            else if (randomValue < 80)
            {
                enemyPrefab = _aimedEnemyPrefab;
            }
            // Homing 20%
            else
            {
                enemyPrefab = _homingEnemyPrefab;
            }

            Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }
}