using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
    public void GoToMainMenu() 
    {
       
        ScoreManager.scoreValue = 0;
        
      
        SceneManager.LoadScene("MainMenu");
    }
   

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
