using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager 
{
    public enum Sound
    {
        Move,
        Attack,
        Hit,
        Die
    }
    
    public static SoundClips soundClipsInstance;

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.Play();
    }
    
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundClips.SoundAudioClip soundAudioClip in soundClipsInstance.SoundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}
