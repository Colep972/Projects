using UnityEngine;

public class DayNightCycles : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0, 24)] public float timeOfDay = 6f; // Start at 6 AM
    public float dayLengthInMinutes = 10f; // real-time duration of one full cycle

    [Header("Lighting")]
    public Light sun; // Assign your Directional Light
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Skybox")]
    public Material skyboxMaterial;
    public Gradient skyTintColor; // controls sky tint
    public Gradient groundColor;  // controls horizon/ground color

    void Update()
    {
        timeOfDay += (24 / (dayLengthInMinutes * 60)) * Time.deltaTime;
        if (timeOfDay >= 24) timeOfDay -= 24;

        UpdateLighting();
    }

    void UpdateLighting()
    {
        float t = timeOfDay / 24f;

        sun.transform.rotation = Quaternion.Euler(new Vector3((t * 360f) - 90, 170, 0));

        sun.color = sunColor.Evaluate(t);
        sun.intensity = sunIntensity.Evaluate(t);

        RenderSettings.skybox.SetColor("_SkyTint", skyTintColor.Evaluate(t));
        RenderSettings.skybox.SetColor("_GroundColor", groundColor.Evaluate(t));
        DynamicGI.UpdateEnvironment(); // refresh lighting in realtime
    }
}
