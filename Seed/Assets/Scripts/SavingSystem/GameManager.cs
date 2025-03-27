using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep SaveSystem between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public List<Upgrade> _upgrades;

    private void Start()
    {
        if (SaveSystem.Instance.SaveExists())
        {
            SaveSystem.Instance.Load();
        }
    }
}
