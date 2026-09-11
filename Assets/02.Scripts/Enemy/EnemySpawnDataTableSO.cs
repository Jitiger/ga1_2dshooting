using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnDataTableSO", menuName = "Scriptable Objects/EnmySpawnDataTableSO")]
public class EnemySpawnDataTableSO : ScriptableObject
{
    public EnemySpawnData[] spawnDatas;
}