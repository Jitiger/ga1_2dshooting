using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 스폰 데이터")]
    [SerializeField]
    private EnemySpawnDataTableSO _spawnDataTable;

    [Header("스폰 간격")]
    [SerializeField]
    private float _spawnInterval = 3f;

    private float _timer = 0f;

    [Header("스폰 위치")]
    [SerializeField]
    private float _spawnMaxPositionX = 0f;

    [SerializeField]
    private float _spawnMinPositionX = 0f;

    [SerializeField]
    private float _spawnMaxPositionY = 0f;

    [SerializeField]
    private float _spawnMinPositionY = 0f;

    [Header("생성할 적의 수")]
    [SerializeField]
    private int _enemyCount = 3;


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
            // 랜덤 위치
            float randomX = Random.Range(
                _spawnMinPositionX,
                _spawnMaxPositionX
            );

            float randomY = Random.Range(
                _spawnMinPositionY,
                _spawnMaxPositionY
            );

            Vector2 spawnPosition =
                new Vector2(randomX, randomY);

            // 가중치에 따라 적 선택
            GameObject enemyPrefab =
                SelectRandomEnemy();

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


    private GameObject SelectRandomEnemy()
    {
        // 데이터 테이블 확인
        if (_spawnDataTable == null)
        {
            Debug.LogError(
                "Enemy Spawn DataTable이 없음."
            );

            return null;
        }

        if (_spawnDataTable.spawnDatas == null ||
            _spawnDataTable.spawnDatas.Length == 0)
        {
            Debug.LogError(
                "Enemy Spawn Data가 비어 있음."
            );

            return null;
        }


        // 1. 전체 가중치 더하기
        int totalWeight = 0;

        foreach (EnemySpawnData data
                 in _spawnDataTable.spawnDatas)
        {
            totalWeight += data.Weight;
        }


        if (totalWeight <= 0)
        {
            Debug.LogError(
                "Weight의 합은 0보다 커야 함."
            );

            return null;
        }


        // 2. 전체 가중치 범위에서 랜덤 값
        int randomWeight =
            Random.Range(0, totalWeight);


        // 3. 누적 가중치로 적 선택
        int cumulativeWeight = 0;

        foreach (EnemySpawnData data
                 in _spawnDataTable.spawnDatas)
        {
            cumulativeWeight += data.Weight;

            if (randomWeight < cumulativeWeight)
            {
                return data.EnemyPrefab;
            }
        }


        return null;
    }
}