using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject settingsMenu; // Panel du menu de paramètres
    [SerializeField] private KeyCode toggleKey = KeyCode.P; // Touche pour ouvrir/fermer le menu

    private bool isMenuOpen = false;

    private void Update()
    {
        // Vérifie si la touche définie a été pressée
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        // Alterne entre ouvert et fermé
        isMenuOpen = !isMenuOpen;
        settingsMenu.SetActive(isMenuOpen);
        

        // Met en pause ou reprend le jeu si nécessaire
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

