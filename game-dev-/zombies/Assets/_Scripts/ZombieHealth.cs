using UnityEngine;

public class ZombieHealth : MonoBehaviour 
{
    public int health = 2; 

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (health <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        
        ZombieSpawner spawner = GameObject.FindObjectOfType<ZombieSpawner>();
        if (spawner != null) 
        {
            spawner.ZombieDied();
        }

       
        ScoreManager.scoreValue += 10;
        
        
        Destroy(gameObject); 
    }
}