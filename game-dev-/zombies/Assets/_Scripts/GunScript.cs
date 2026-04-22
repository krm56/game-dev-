using UnityEngine;

public class GunScript : MonoBehaviour 
{
    public float range = 30f;

    public ParticleSystem muzzleFlash; 
   
    private AudioSource gunAS;

    void Start() {
        gunAS = GetComponent<AudioSource>();
        
   
        if (muzzleFlash != null) muzzleFlash.Stop();
    }

    void Update() {
        if (Time.timeScale == 0f) return;

        if (Input.GetButtonDown("Fire1")) { 
            Shoot();
        }
    }

    void Shoot() {
        if (gunAS != null) gunAS.Play();

  
        if (muzzleFlash != null) {
            muzzleFlash.Play();
        }

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