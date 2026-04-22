using UnityEngine;

public class GraphicsSettings : MonoBehaviour 
{
   
    public void SetLow() 
    {
        QualitySettings.SetQualityLevel(0, true);
        
    }

    public void SetMedium() 
    {
        QualitySettings.SetQualityLevel(2, true);
        
    }

    public void SetHigh() 
    {
        QualitySettings.SetQualityLevel(5, true);
        
    }
}