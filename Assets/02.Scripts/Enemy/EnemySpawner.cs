using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float SpawnTime = 2f;
    public float SpawnTimer = 0;
    public float SpawnMaxPositionX;
    public float SpawnMinPositionX;
    public float SpawnPositionY;
    public float MoveSpeed = 3f;

    private void Start()
    {
    }

    private void Update()
    {
        EnemySpawner();
    }

    private void EnemySpawner()
    {
        if (SpawnTimer <= 0)
        {
            float randomX = Random.Range(SpawnMinPositionX, SpawnMaxPositionX);
            Vector2 spawnPosition = new Vector2(randomX, SpawnPositionY);

            //여기에 적 생성 코드 넣어야함 ㅠ ㅠ

            Vector2 direction = Vector2.down;
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
            SpawnTimer = SpawnTime;
        }
    }
}