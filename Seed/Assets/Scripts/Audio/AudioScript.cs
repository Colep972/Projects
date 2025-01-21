using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    [Header("Audio Mixer Configuration")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float _volume;

    private const string SFX_VOLUME_PARAMETER = "SFXVolume";

    private void Start()
    {
        if (audioMixer == null)
        {
            audioMixer = Resources.Load<AudioMixer>("GameAudioMixer");
        }

        // Utiliser la valeur de StaticClass si elle existe, sinon utiliser les PlayerPrefs
        _volume = StaticClass._volume;
        if (_volume <= 0 && StaticClass._tmpvolume == _volume)
        {
            _volume = 50f;
            StaticClass._tmpvolume = _volume;
            PlayerPrefs.GetFloat("SFXVolume", _volume);

            StaticClass._volume = _volume; // Synchroniser avec la classe statique
        }

        InitializeSlider();
    }

    public void InitializeWithValue(float value)
    {
        _volume = value;
        InitializeSlider();
    }

    private void InitializeSlider()
    {
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 100f;
            volumeSlider.value = _volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(_volume);
        }
    }

    public void SetVolume(float sliderValue)
    {
        _volume = sliderValue;
        StaticClass._volume = sliderValue; // Synchroniser avec la classe statique
        float dB = LinearToDecibels(sliderValue);
        audioMixer.SetFloat(SFX_VOLUME_PARAMETER, dB);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void PlaySound(AudioSource audioSource)
    {
        // Check both audioSource and volumeSlider
        if (audioSource == null)
        {
            Debug.LogWarning("Attempted to play null AudioSource");
            return;
        }

        if (volumeSlider == null)
        {
            Debug.LogWarning("VolumeSlider is not assigned");
            audioSource.Play(); // Play with default volume if slider isn't available
            return;
        }

        audioSource.volume = volumeSlider.value / 100f;
        audioSource.Play();
    }

    private void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public float getVolume()
    {
        return volumeSlider.value;
    }

    private float LinearToDecibels(float linearValue)
    {
        if (linearValue <= 0f) return -80f;
        return 20f * Mathf.Log10(linearValue / 100f);
    }

    private float DecibelsToLinear(float dB)
    {
        if (dB <= -80f) return 0f; 
        return Mathf.Pow(10f, dB / 20f) * 100f;
    }
}
