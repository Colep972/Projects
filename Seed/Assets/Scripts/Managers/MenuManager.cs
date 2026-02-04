using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Button loadGameButton;

    private void Awake()
    {
        GameObject loadGameButtonObject = GameObject.Find("Load Game");
        if (loadGameButtonObject != null)
        {
            loadGameButton = loadGameButtonObject.GetComponent<Button>();
            if (loadGameButton != null)
            {
                loadGameButton.interactable = System.IO.File.Exists(Application.persistentDataPath + "/save.json");
            }
        }
    }

    private void Start()
    {
        if (loadGameButton != null)
        {
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
        GameState.shouldLoadGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void LoadGame()
    {
        GameState.shouldLoadGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }
}
