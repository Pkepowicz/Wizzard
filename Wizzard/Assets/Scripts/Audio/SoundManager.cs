using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager
{
    public static void PlaySound(string sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
        
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }
    
    private static AudioClip GetAudioClip(string sound)
    {
        foreach (SoundClips.SoundAudioClip soundAudioClip in SoundClips.instance.SoundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log($"Variable '{ sound }' not specified");
        return null;
    }
}
