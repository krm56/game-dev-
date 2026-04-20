using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; 
using UnityEngine.UI;    

public class MainMenu : MonoBehaviour
{

    public AudioMixer mainMixer;
    public Slider volumeSlider;

    public void SetVolume(float sliderValue)
    {
        
        float dBValue = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        mainMixer.SetFloat("MasterVol", dBValue);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("map1 1"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit Pressed"); 
        Application.Quit();
    }

    void Awake()
{
    Time.timeScale = 1f; 
    PauseMenu.isPaused = false;
    Cursor.lockState = CursorLockMode.None; 
    Cursor.visible = true;
}
}