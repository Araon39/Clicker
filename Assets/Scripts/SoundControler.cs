using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip clickSound;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void onButtonClickAudio()
    {
        audio.PlayOneShot(clickSound);
    }
}
