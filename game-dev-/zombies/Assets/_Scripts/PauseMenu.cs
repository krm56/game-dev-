using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; 

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu; 
    public Transform player; 
    public static bool isPaused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SaveGame() {
        string path = Application.persistentDataPath + "/save.txt";
        string saveData = SceneManager.GetActiveScene().buildIndex + "|" + 
                          player.position.x + "|" + player.position.y + "|" + player.position.z;
        
        File.WriteAllText(path, saveData);
        Debug.Log("Game Saved to " + path);
    }


    public void LoadGame() {
        string path = Application.persistentDataPath + "/save.txt";
        if (File.Exists(path)) {
            string[] data = File.ReadAllText(path).Split('|');
          
            SceneManager.LoadScene(int.Parse(data[0]));

        }
    }

    public void QuitToMenu() {
        Time.timeScale = 1f;
        isPaused = false;
       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        SceneManager.LoadScene(0); 
    }
}
