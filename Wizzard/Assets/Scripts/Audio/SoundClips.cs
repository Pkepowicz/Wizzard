using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClips : MonoBehaviour
{
    public static SoundClips instance;
    public SoundAudioClip[] SoundAudioClips;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [System.Serializable]
    public struct SoundAudioClip
    {
        public string sound;
        public AudioClip audioClip;
    }
}
