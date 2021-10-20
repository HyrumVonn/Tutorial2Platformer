using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThing : MonoBehaviour
{
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    public void Play1()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    public void Play2()
    {
        musicSource.clip = musicClipTwo;
        musicSource.Play();
    }


}
