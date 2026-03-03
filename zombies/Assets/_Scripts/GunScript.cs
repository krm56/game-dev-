using UnityEngine;

public class GunScript : MonoBehaviour 
{
    public float range = 50f;

    void Update() {
        if (Input.GetButtonDown("Fire1")) { // Left Click to Shoot
            Shoot();
        }
    }

    void Shoot() {
        RaycastHit hit;
        // Shoots a ray from the center of the camera
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)) {
            if (hit.transform.CompareTag("Zombie")) {
                Destroy(hit.transform.gameObject); // Zombie dies!
                Debug.Log("Zombie Killed!");
            }
        }
    }
}