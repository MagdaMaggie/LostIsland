using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDuration = 120f;
    public float timeMultiplier = 1f;

    [Header("Sun and Moon")]
    public Light sunLight; 
    public Light moonLight; 

    [Header("Skybox Intensity")]
    public AnimationCurve sunIntensityCurve; 
    public AnimationCurve moonIntensityCurve; 

    private float currentTime; 

    void Start()
    {
        if (sunLight == null || moonLight == null)
        {
            Debug.LogError("Please assign Sun and Moon lights in the Inspector.");
        }

        currentTime = 0f;
    }

    void Update()
    {
        // Update time
        currentTime += (Time.deltaTime / dayDuration) * timeMultiplier;
        if (currentTime >= 1f)
        {
            currentTime = 0f; 
        }


        RotateSunAndMoon();


        AdjustLighting();
    }

    private void RotateSunAndMoon()
    {
        float sunAngle = currentTime * 360f; 
        sunLight.transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);

        float moonAngle = (currentTime + 0.5f) * 360f; 
        moonLight.transform.rotation = Quaternion.Euler(moonAngle - 90f, 170f, 0f);
    }

    private void AdjustLighting()
    {
        
        float sunIntensity = sunIntensityCurve.Evaluate(currentTime);
        float moonIntensity = moonIntensityCurve.Evaluate(currentTime);

        sunLight.intensity = sunIntensity;
        moonLight.intensity = moonIntensity;


        sunLight.enabled = sunIntensity > 0.01f;
        moonLight.enabled = moonIntensity > 0.01f;
    }
}
