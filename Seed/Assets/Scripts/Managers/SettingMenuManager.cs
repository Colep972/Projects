using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject settingsMenu; // Panel du menu de param�tres
    [SerializeField] private KeyCode toggleKey = KeyCode.P; // Touche pour ouvrir/fermer le menu

    private bool isMenuOpen = false;

    private void Update()
    {
        // V�rifie si la touche d�finie a �t� press�e
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        // Alterne entre ouvert et ferm�
        isMenuOpen = !isMenuOpen;
        settingsMenu.SetActive(isMenuOpen);
        

        // Met en pause ou reprend le jeu si n�cessaire
        //Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void CloseMenu()
    {
        // Ferme explicitement le menu (par exemple via un bouton)
        isMenuOpen = false;
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}

