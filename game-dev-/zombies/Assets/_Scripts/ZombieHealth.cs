using UnityEngine;

public class ZombieHealth : MonoBehaviour 
{
    public int health = 2; 
    public int pointsGiver = 10; 
    public int hp => health; 

    public void TakeDamage(int damage) 
    {
        if (health <= 0) return;

        health -= damage;

        if (health <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        ZombieSpawner spawner = GameObject.FindObjectOfType<ZombieSpawner>();
        if (spawner != null) spawner.ZombieDied();

      
        ScoreManager.scoreValue += pointsGiver;

        RagdollController rc = GetComponent<RagdollController>();
        if (rc != null) 
        {
            rc.EnableRagdoll();
        }

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        Destroy(gameObject, 10f);
    }
}