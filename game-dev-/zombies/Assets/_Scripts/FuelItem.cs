using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FuelItem : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other) 
    {
       
        if (other.CompareTag("Player")) 
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();
            
            if (pm != null)
            {
                pm.PickUpFuel(); 
                Destroy(gameObject); 
            }
        }
    }
}