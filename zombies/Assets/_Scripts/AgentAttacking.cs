using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AgentAttacking : MonoBehaviour 
{
    public float damageRate = 20f; // Damage per second

    // If your Zombie collider is a Trigger
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager manager = other.GetComponent<PlayerManager>();
            if (manager != null)
            {
                // Damages the player over time while touching
                manager.TakeDamage(damageRate * Time.deltaTime);
            }
        }
    }

    // If your Zombie collider is NOT a Trigger
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.gameObject.GetComponent<PlayerManager>();
            if (manager != null)
            {
                manager.TakeDamage(damageRate * Time.deltaTime);
            }
        }
    }
}