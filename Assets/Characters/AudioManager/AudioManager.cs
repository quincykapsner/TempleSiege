using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip playerAttack;
    public AudioClip orcAttack; 
    public AudioClip win; 
    public AudioClip gameOver; 
    public AudioClip criticalHealth; 
    public AudioClip enter;

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
}
