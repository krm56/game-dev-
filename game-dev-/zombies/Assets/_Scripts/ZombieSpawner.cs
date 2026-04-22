using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
    public GameObject zombiePrefab; 
    public float spawnRate = 1f;  
    public int maxZombies = 50;     
    public float spawnRadius = 5f; 
    
    private int currentZombies = 0;
    private float nextSpawnTime;

    void Update() {
    
       
        if (Time.time > nextSpawnTime && currentZombies < maxZombies && Time.timeScale != 0) {
            SpawnZombie();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnZombie() {
      
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
        Vector3 spawnPos = transform.position + randomOffset;

        Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        currentZombies++;
    }

    
    public void ZombieDied() {
        currentZombies--;
    }
}