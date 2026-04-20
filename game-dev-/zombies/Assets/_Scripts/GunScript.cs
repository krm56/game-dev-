using UnityEngine;

public class GunScript : MonoBehaviour 
{
    public float range = 50f;

    void Update() {
        
        if (Time.timeScale == 0f) return;

        if (Input.GetButtonDown("Fire1")) { 
            Shoot();
        }
    }

    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)) {
            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();
            
         
            if (zombie == null) zombie = hit.transform.GetComponentInParent<ZombieHealth>();

            if (zombie != null) {
                zombie.TakeDamage(1); 
            }
        }
    }
}