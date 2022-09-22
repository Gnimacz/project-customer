using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource sound = new AudioSource();
    [SerializeField] private AudioClip clip;


    public void Sound()
    {
        sound.PlayOneShot(clip);
    }

    public void StopSound()
    {
        sound.Stop(clip);
    }
}
