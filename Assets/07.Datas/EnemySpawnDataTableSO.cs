using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnDataTable", menuName = "Scriptable Objects/EnemySpawnDataTable")]
public class EnemySpawnDataTableSO : ScriptableObject
{
    public EnemySpawnData[] Datas;
}