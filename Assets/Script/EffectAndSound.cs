using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAndSound : MonoBehaviour
{
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundEffectPlay(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    
}
