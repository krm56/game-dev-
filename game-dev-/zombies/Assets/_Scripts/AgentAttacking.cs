using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AgentAttacking : MonoBehaviour 
{
    public float damageAmount = 20f; 
    public float attackCooldown = 1.2f; 
    private float nextDamageTime;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerManager manager = other.GetComponent<PlayerManager>();
            if (manager != null)
            {
                manager.TakeDamage(damageAmount);
                
              
                nextDamageTime = Time.time + attackCooldown;
                
                
            }
        }
    }
}