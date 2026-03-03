using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlaneWin : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager pm = other.GetComponent<PlayerManager>();
        
        if (pm != null && pm.fuelCount >= 3)
        {
            Debug.Log("You Escaped!");
            SceneManager.LoadScene(0); // Go to Menu
        }
    }
}