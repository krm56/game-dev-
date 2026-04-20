using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
namespace UnityStandardAssets.Characters.FirstPerson
{
public class PlayerManager : MonoBehaviour 
{
    [Header("UI Links")]
    public Slider healthBar;
    public Slider sprintBar;
    public TextMeshProUGUI fuelText;

    [Header("Stats")]
    public float hp = 100f;
    public float energy = 100f;
    public int fuelCount = 0;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;

    void Start() {
        currentSpeed = walkSpeed;
        
        fuelText.text = "Fuel: 0/3";
    }

    void Update() {

        if (PauseMenu.isPaused) return;
       
        if (Input.GetKey(KeyCode.LeftShift) && energy > 0) {
            energy -= 25f * Time.deltaTime; 
            currentSpeed = sprintSpeed;
        } else {
            energy += 15f * Time.deltaTime; 
            currentSpeed = walkSpeed;
        }

        energy = Mathf.Clamp(energy, 0, 100);
        sprintBar.value = energy;

        ApplySpeedToController();

      
        if (fuelCount >= 3) {
           
        }
    }

    void ApplySpeedToController() {
    }

    public void TakeDamage(float amount) {
         hp -= amount;
         healthBar.value = hp;

    
        if (hp <= 0) {
     
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;
        
        
           SceneManager.LoadScene("GameOver"); 
    }
}

    
    public void PickUpFuel() {
        fuelCount++;
        fuelText.text = "Fuel: " + fuelCount + "/3";
        
        Debug.Log("Fuel Collected! Total: " + fuelCount);
    }
}
}