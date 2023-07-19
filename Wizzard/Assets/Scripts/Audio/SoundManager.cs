using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        Move,
        Attack,
        Hit,
        Die
    }

    [SerializeField]
    public SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    public void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
        
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }
    
    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound not specified");
        return null;
    }
}
