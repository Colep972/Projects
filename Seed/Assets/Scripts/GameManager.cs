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

    private void Start()
    {
        Load();
    }


    public void Load()
    {
        if (GameState.shouldLoadGame && SaveSystem.Instance.SaveExists())
        {
            SaveSystem.Instance.Load();
        }
        else
        {
            Debug.Log("New Game started.");
        }
    }

    public bool IsSaved()
    {
        return SaveSystem.Instance != null && SaveSystem.Instance.SaveExists();
    }
}
