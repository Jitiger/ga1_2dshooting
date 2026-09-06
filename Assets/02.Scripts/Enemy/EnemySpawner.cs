using UnityEngine;

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private class EnemySpawnData
    {
        public Enemy EnemyPrefab;
        public int Weight;
    }

    // 필요 속성
    [Header("스폰 적 프리팹과 가중치")]
    [SerializeField] private EnemySpawnData[] _enemySpawnData;

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

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;

            // 적 생성 간격은 기존처럼 균등 난수 사용
            _spawnInterval = Random.Range(1f, 3f);

            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            // 랜덤한 생성 위치 선택
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

            // 가중치를 이용해서 생성할 적 선택
            Enemy enemyPrefab = SelectRandomEnemy();

            if (enemyPrefab == null)
            {
                return;
            }

            Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }

    private Enemy SelectRandomEnemy()
    {
        // 배열이 비어 있는지 확인
        if (_enemySpawnData == null || _enemySpawnData.Length == 0)
        {
            Debug.LogError("Enemy Spawn Data가 비어 있음.");
            return null;
        }

        // 전체 Weight 계산
        int totalWeight = 0;

        foreach (EnemySpawnData enemy in _enemySpawnData)
        {
            totalWeight += enemy.Weight;
        }

        if (totalWeight <= 0)
        {
            Debug.LogError("적 Weight의 합은 0보다 커야함.");
            return null;
        }

        // 전체 Weight 범위에서 난수 생성
        int randomValue = Random.Range(0, totalWeight);

        // Weight를 차례대로 빼면서 적 선택
        foreach (EnemySpawnData enemy in _enemySpawnData)
        {
            randomValue -= enemy.Weight;

            if (randomValue < 0)
            {
                return enemy.EnemyPrefab;
            }
        }

        // 혹시 모를 경우 마지막 적 반환
        return _enemySpawnData[_enemySpawnData.Length - 1].EnemyPrefab;
    }
}
