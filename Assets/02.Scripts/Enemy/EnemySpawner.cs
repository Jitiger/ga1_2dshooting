using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 스폰 데이터")]
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;
    [SerializeField] private EnemyBalanceDaTatableSO _balanceDatatable;

    [Header("스폰 간격")]
    [SerializeField] private float _spawnInterval = 3f;

    private float _timer;

    [Header("스폰 위치")]
    [SerializeField] private float _spawnMaxPositionX;
    [SerializeField] private float _spawnMinPositionX;
    [SerializeField] private float _spawnMaxPositionY;
    [SerializeField] private float _spawnMinPositionY;

    [Header("생성할 적의 수")]
    [SerializeField] private int _enemyCount = 3;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _spawnInterval)
        {
            return;
        }

        _timer = 0f;
        _spawnInterval = Random.Range(1f, 3f);

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
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

            GameObject enemyPrefab = SelectRandomEnemy();

            if (enemyPrefab == null)
            {
                return;
            }

            GameObject spawnedEnemy = Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );

            Enemy enemy = spawnedEnemy.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.SetHealthBalance(
                    GetHealthMultiplier()
                );
            }
        }
    }

    private GameObject SelectRandomEnemy()
    {
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

        int randomWeight = Random.Range(
            0,
            totalWeight
        );

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

    private float GetHealthMultiplier()
    {
        int bestscore = ScoreManager.Instance.BestScore;
        foreach (EnemyBalanceData data in _balanceDatatable.Datas)
        {
            if (bestscore < data.RequiredScore)
            {
                return data.HealthMultiplier;
            }
        }
        // int lastIndex = _balanceDataTable.Datas.Length-1;
        //return _balanceDataTable.Datas[lastIndex].HealthMultiplier;
    }

}