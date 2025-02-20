namespace MiniGame2 
{
    using UnityEngine;

    public class EnemySpawner : MonoBehaviour 
   {
       [SerializeField] private GameObject enemyPrefab;
       [SerializeField] private float spawnRate = 1f;
       [SerializeField] private float spawnRadius = 10f;
       private float nextSpawnTime = 0f;

       void Update() 
       {
           if (Time.time > nextSpawnTime) 
           {
               SpawnEnemy();
               nextSpawnTime = Time.time + spawnRate;
               
               // 난이도 증가
               spawnRate = Mathf.Max(spawnRate * 0.95f, 0.3f);
           }
       }

       void SpawnEnemy() 
       {
           // 원형으로 랜덤한 위치에서 스폰
           float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
           Vector3 spawnPos = new Vector3(
               Mathf.Cos(angle) * spawnRadius,
               Mathf.Sin(angle) * spawnRadius,
               0
           );

           Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
       }
   }
}
