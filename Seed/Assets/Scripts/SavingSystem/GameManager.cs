using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public SavingManager _save;

    public void SaveGame()
    {
        GameData gameData = _save.GetGameData();
        SaveSystem.Save(gameData); // Utilisez votre système de sauvegarde JSON/Binaire
        Debug.Log("Jeu sauvegardé.");
    }

    public void LoadGame()
    {
        GameData gameData = SaveSystem.Load();
        _save.LoadGameData(gameData);
        Debug.Log("Jeu chargé.");
    }

}
