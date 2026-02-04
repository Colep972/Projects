using System;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Sun & Sky")]
    public Light sun;
    public Material skyboxMaterial;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    public Gradient skyTint;
    public Gradient groundColor;

    [Header("Time Settings")]
    [Tooltip("In real hours: how long one full game day lasts in real time")]
    public float realHoursPerGameDay = 12f;

    [Header("Debug Info (Read-Only)")]
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private int gameDay;
    [SerializeField] private int gameYear = 1;
    [SerializeField] private SeasonManager seasonManager;
    [SerializeField] private SeasonManager.Season currentSeason;
    [SerializeField, Range(0f, 1f)] private float seasonProgress;

    private const int GameDaysPerYear = 730; // 2 real years per game year
    private DateTime epoch = new DateTime(2025, 1, 1, 0, 0, 0); // fixed world start date

    void Start()
    {
        if (skyboxMaterial != null)
            RenderSettings.skybox = skyboxMaterial;

        // Auto-find SeasonManager if not linked in inspector
        if (seasonManager == null)
            seasonManager = FindObjectOfType<SeasonManager>();
    }

    void Update()
    {
        UpdateGameClock();
        UpdateLighting();
        UpdateSeason();
    }

    void UpdateGameClock()
    {
        // ⏱ Real time since epoch
        TimeSpan realElapsed = DateTime.Now - epoch;

        // ⏳ Convert to game time (12 real hours = 1 in-game day)
        double gameDaysElapsed = realElapsed.TotalHours / realHoursPerGameDay;

        gameDay = (int)gameDaysElapsed;
        timeOfDay = (float)((gameDaysElapsed - gameDay) * 24f);
        gameYear = (gameDay / GameDaysPerYear) + 1;
    }

    void UpdateLighting()
    {
        float t = timeOfDay / 24f;

        // ☀️ Sun rotation
        if (sun != null)
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3((t * 360f) - 90, 170, 0));
            sun.color = sunColor.Evaluate(t);
            sun.intensity = sunIntensity.Evaluate(t);
        }

        // 🌤 Skybox colors
        if (skyboxMaterial)
        {
            RenderSettings.skybox.SetColor("_SkyTint", skyTint.Evaluate(t));
            RenderSettings.skybox.SetColor("_GroundColor", groundColor.Evaluate(t));
        }

        DynamicGI.UpdateEnvironment();
    }

    void UpdateSeason()
    {
        if (seasonManager == null) return;

        currentSeason = seasonManager.GetCurrentSeason();
        seasonProgress = seasonManager.GetSeasonProgress();
    }

    public int GetGameDay() => gameDay;
    public SeasonManager.Season GetCurrentSeason() => currentSeason;
}
