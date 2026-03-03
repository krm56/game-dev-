using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; // Needed to "see" the PlayerManager

public class FuelItem : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other) 
    {
        // Check if the thing hitting the fuel is the Player
        if (other.CompareTag("Player")) 
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();
            
            if (pm != null)
            {
                pm.PickUpFuel(); // Tell the UI to update to 1/3, 2/3, etc.
                Destroy(gameObject); // Remove the fuel from the map
            }
        }
    }
}