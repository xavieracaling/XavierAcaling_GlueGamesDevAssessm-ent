using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;    
    public Transform Player;         
    public float SpawnRadius = 10f;  
    public float MinSpawnDistance = 3f; 
    public float SpawnInterval = 2f;  
    public Transform GameContainer;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), SpawnInterval, SpawnInterval); 
    }

    private void SpawnEnemy()
    {
        if(GameManager.Instance.EndGame) return;
        Vector2 spawnPosition;

        do
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized; 
            float randomDistance = Random.Range(MinSpawnDistance, SpawnRadius);
            spawnPosition = (Vector2)Player.position + randomDirection * randomDistance;
        }
        while (Vector2.Distance(spawnPosition, Player.position) < MinSpawnDistance); 

        Instantiate(EnemyPrefab, spawnPosition,Quaternion.identity, GameContainer);
    }
}
