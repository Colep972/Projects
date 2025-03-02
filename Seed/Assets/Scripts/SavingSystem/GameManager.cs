using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData _gameData;
    public SavingManager _save; 
    public List<Upgrade> _upgrades;

    private void Start()
    {
        if (SaveSystem.SaveExists())
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        _gameData = _save.GetGameData();
        SaveSystem.Save(_gameData, _upgrades);
        Debug.Log("Game saved successfully.");
    }

    public void LoadGame()
    {
        if (SaveSystem.SaveExists())
        {
            _gameData = SaveSystem.Load(_upgrades);
            _save.LoadGameData(_gameData);
            Debug.Log("Game loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No save data found!");
        }
    }

}
