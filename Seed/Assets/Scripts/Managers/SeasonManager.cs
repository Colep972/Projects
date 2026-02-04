using UnityEngine;
using System;

public class SeasonManager : MonoBehaviour
{
    public enum Season { Winter, Spring, Summer, Autumn }

    [Header("Debug Info (Read-Only)")]
    [SerializeField] private Season currentSeason;
    [SerializeField, Range(0f, 1f)] private float seasonProgress; // % of season passed

    void Update()
    {
        UpdateSeason();
    }

    void UpdateSeason()
    {
        DateTime now = DateTime.Now;
        int dayOfYear = now.DayOfYear;

        // Approximate equinox/solstice dates for Northern Hemisphere
        int springStart = new DateTime(now.Year, 3, 20).DayOfYear;
        int summerStart = new DateTime(now.Year, 6, 21).DayOfYear;
        int autumnStart = new DateTime(now.Year, 9, 22).DayOfYear;
        int winterStart = new DateTime(now.Year, 12, 21).DayOfYear;

        if (dayOfYear >= springStart && dayOfYear < summerStart)
        {
            currentSeason = Season.Spring;
            seasonProgress = Mathf.InverseLerp(springStart, summerStart, dayOfYear);
        }
        else if (dayOfYear >= summerStart && dayOfYear < autumnStart)
        {
            currentSeason = Season.Summer;
            seasonProgress = Mathf.InverseLerp(summerStart, autumnStart, dayOfYear);
        }
        else if (dayOfYear >= autumnStart && dayOfYear < winterStart)
        {
            currentSeason = Season.Autumn;
            seasonProgress = Mathf.InverseLerp(autumnStart, winterStart, dayOfYear);
        }
        else
        {
            // Winter: wrap around New Year
            currentSeason = Season.Winter;

            if (dayOfYear >= winterStart)
                seasonProgress = Mathf.InverseLerp(winterStart, 365, dayOfYear);
            else
                seasonProgress = Mathf.InverseLerp(1, springStart, dayOfYear);
        }
    }
    public Season GetCurrentSeason() => currentSeason;
    public float GetSeasonProgress() => seasonProgress;
}
