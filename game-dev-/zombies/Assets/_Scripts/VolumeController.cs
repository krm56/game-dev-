using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour 
{
    [Header("Setup")]
    public AudioMixer mixer; 
    public string parameterName = "MyVolume"; 

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
    }

    void Start() 
    {
        
        slider.onValueChanged.AddListener(SetLevel);
        
        
        float currentVol;
        if (mixer.GetFloat(parameterName, out currentVol))
        {
            slider.value = Mathf.Pow(10, currentVol / 20);
        }
    }

    public void SetLevel(float sliderValue) 
    {
        mixer.SetFloat(parameterName, Mathf.Log10(sliderValue) * 20);
    }
}