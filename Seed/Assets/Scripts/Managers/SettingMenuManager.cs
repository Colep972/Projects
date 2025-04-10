using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject settingsMenu;

    private bool isMenuOpen = false;

    private void Update()
    {
      
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        settingsMenu.SetActive(isMenuOpen);
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}

