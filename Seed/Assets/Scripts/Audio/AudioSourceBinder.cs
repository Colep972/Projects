using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceBinder : MonoBehaviour
{
    [Header("Audio Configuration")]
    [SerializeField] private AudioMixer audioMixer; // Assigne ton AudioMixer dans l'Inspector
    [SerializeField] private string audioMixerGroupName = "SFX"; // Nom du groupe dans l'AudioMixer
    [SerializeField] private GameObject prefabWithAudioSource; // Le prefab contenant l'AudioSource


    private void Start()
    {
        // Vérifie si le prefab est assigné
        if (prefabWithAudioSource == null)
        {
            Debug.LogError("Prefab avec AudioSource non assigné !");
            return;
        }

        // Récupère le groupe de l'AudioMixer
        AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups(audioMixerGroupName);
        if (audioMixerGroups.Length == 0)
        {
            Debug.LogError($"Le groupe '{audioMixerGroupName}' est introuvable dans l'AudioMixer !");
            return;
        }
    }
}

