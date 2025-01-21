using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetManager : MonoBehaviour
{
    public void ResetGame()
    {
        // R�cup�rer la sc�ne active
        Scene currentScene = SceneManager.GetActiveScene();

        // Recharger la sc�ne active
        SceneManager.LoadScene(currentScene.name);

        // Optionnel : R�initialiser d'autres syst�mes ici (si n�cessaire)
        ResetGameState();
    }

    private void ResetGameState()
    {
        // R�initialiser les param�tres globaux si n�cessaire
        PlayerPrefs.DeleteAll(); // Supprime toutes les pr�f�rences enregistr�es
        Debug.Log("Game state has been reset!");
    }
}
