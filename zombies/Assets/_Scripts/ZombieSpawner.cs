using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Drag your Zombie from the Project folder here
    public int count = 100;         // Number of zombies
    public float spawnRadius = 50f; // How far away they can spawn

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        // Pick a random spot
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0,
            Random.Range(-spawnRadius, spawnRadius)
        );

        // Spawn the zombie
        Instantiate(zombiePrefab, randomPos, Quaternion.identity);
    }
}