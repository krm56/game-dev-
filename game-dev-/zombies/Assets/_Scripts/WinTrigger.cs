using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class WinTrigger : MonoBehaviour 
{
    public string nextSceneName; 
    public int requiredFuel = 3;

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager pm = other.GetComponent<PlayerManager>();
        
        if (pm != null && pm.fuelCount >= requiredFuel)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}