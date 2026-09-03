using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public float SpawnTime = 2f;
    public float SpawnTimer = 0;

    public float SpawnMaxPositionX;
    public float SpawnMinPositionX;
    public float SpawnPositionY;

    private void Update()
    {
        SpawnTimer -= Time.deltaTime;

        EnemySpawner();
    }

    private void EnemySpawner()
    {
        if (SpawnTimer <= 0)
        {
            float randomX = Random.Range(SpawnMinPositionX, SpawnMaxPositionX);
            Vector2 spawnPosition = new Vector2(randomX, SpawnPositionY);

            int randomEnemy = Random.Range(0, EnemyPrefabs.Length);

            GameObject enemy = Instantiate(EnemyPrefabs[randomEnemy]);
            enemy.transform.position = spawnPosition;

            SpawnTimer = SpawnTime;
        }
    }
}