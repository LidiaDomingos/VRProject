using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioPlays : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip audioClip;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioClip); 
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not set!");
        }
    }
}
