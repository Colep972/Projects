using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetManager : MonoBehaviour
{
    public void ResetGame()
    {
        // Récupérer la scène active
        Scene currentScene = SceneManager.GetActiveScene();

        // Recharger la scène active
        SceneManager.LoadScene(currentScene.name);

        // Optionnel : Réinitialiser d'autres systèmes ici (si nécessaire)
        ResetGameState();
    }

    private void ResetGameState()
    {
        // Réinitialiser les paramètres globaux si nécessaire
        PlayerPrefs.DeleteAll(); // Supprime toutes les préférences enregistrées
        Debug.Log("Game state has been reset!");
    }
}
