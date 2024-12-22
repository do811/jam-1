using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundPlayer
{
    private static AudioSource audio = GameObject.Find("manager").GetComponent<AudioSource>();
    public static void PlaySound()
    {
        audio.Play();
    }

    public static void StopSound()
    {
        audio.Stop();
    }
}
