using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
  
    public void GoToGameOver() {
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToWinScene() {
        SceneManager.LoadScene("YouWin");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   
    public void LoadLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}