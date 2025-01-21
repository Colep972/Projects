using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/save.json";
    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Données sauvegardées dans " + savePath);
    }

    public static GameData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Données chargées depuis " + savePath);
            return data;
        }
        Debug.LogWarning("Aucune sauvegarde trouvée.");
        return new GameData(); // Retourne une sauvegarde par défaut
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
