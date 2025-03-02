using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Button loadGameButton;
    private GameManager gameManager;

    private void Awake()
    {
        loadGameButton = GameObject.Find("Load Game").GetComponent<Button>();
        loadGameButton.interactable = false;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        if (loadGameButton != null)
        {
            loadGameButton.interactable = SaveSystem.SaveExists();
            if(!loadGameButton.interactable)
            {
                ColorBlock colors = loadGameButton.colors;
                colors.normalColor = new Color(65f, 124f, 24f, 255f); 
                colors.disabledColor = Color.grey; 
                loadGameButton.colors = colors;
            }
        }
        else
        {
            Debug.LogError("Le bouton Load Game n'a pas été trouvé dans la hiérarchie !");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        if (gameManager != null)
        {
            gameManager.LoadGame();
        }
        else
        {
            Debug.LogError("GameManager not found in scene!");
        }
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }
}
