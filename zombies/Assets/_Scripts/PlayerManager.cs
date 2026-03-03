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
        // Set initial text
        fuelText.text = "Fuel: 0/3";
    }

    void Update() {
        // --- SPRINT LOGIC ---
        // You can only sprint if Shift is held AND you have energy
        if (Input.GetKey(KeyCode.LeftShift) && energy > 0) {
            energy -= 25f * Time.deltaTime; 
            currentSpeed = sprintSpeed;
        } else {
            energy += 15f * Time.deltaTime; 
            currentSpeed = walkSpeed;
        }

        // Keep energy between 0 and 100
        energy = Mathf.Clamp(energy, 0, 100);
        sprintBar.value = energy;

        // Apply speed to your controller (See Step 2 below)
        ApplySpeedToController();

        // --- WIN CONDITION ---
        // If you want it to happen instantly when getting 3 fuel:
        if (fuelCount >= 3) {
            // SceneManager.LoadScene(0); 
        }
    }

    void ApplySpeedToController() {
        // This part depends on your specific movement script. 
        // If you are using a standard Character Controller, you use currentSpeed there.
    }

    public void TakeDamage(float amount) {
        hp -= amount;
        healthBar.value = hp;
        if (hp <= 0) {
            SceneManager.LoadScene(0); 
        }
    }

    // UPDATED: Now updates the 0/3 text correctly
    public void PickUpFuel() {
        fuelCount++;
        fuelText.text = "Fuel: " + fuelCount + "/3";
        
        Debug.Log("Fuel Collected! Total: " + fuelCount);
    }
}
}