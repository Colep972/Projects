using UnityEngine;
using System.Collections.Generic;

public class RandomAudioPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    public List<AudioClip> audioClips;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private AudioScript audioScript;
    public float minInterval = 2.0f;
    public float maxInterval = 5.0f;

    private float nextPlayTime;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioClips.Count == 0 || audioSource == null)
        {
            Debug.LogError("AudioClips list is empty or AudioSource is not assigned.");
            return;
        }

        ScheduleNextAudio();
    }

    private void Update()
    {
        if (Time.time >= nextPlayTime)
        {
            PlayRandomAudio();
            ScheduleNextAudio();
        }
    }

    private void PlayRandomAudio()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[randomIndex];

            if (audioScript != null)
            {
                audioScript.PlaySound(audioSource);
            }
            else
            {
                //audioSource.Play();
            }
        }
    }

    private void ScheduleNextAudio()
    {
        nextPlayTime = Time.time + Random.Range(minInterval, maxInterval);
    }
}
