using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClips : MonoBehaviour
{
    public SoundAudioClip[] SoundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
