using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SoundManager
{
    public static void PlaySound(string sound, Vector3 source)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = source;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 6f;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.dopplerLevel = 0f;
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
