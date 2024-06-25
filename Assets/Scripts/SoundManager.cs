using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
     [SerializeField] private List<SoundClip> _soundClips;
    [SerializeField] Dictionary<SoundType, AudioClip> _clips = new Dictionary<SoundType, AudioClip>();

    private void OnEnable()
    {
        EventSystem.PlaySoundClip += HandlePlayingClip;
    }

    private void OnDisable()
    {
       EventSystem.PlaySoundClip += HandlePlayingClip; 
    }

    private void HandlePlayingClip(SoundType soundType)
    {
        foreach (SoundClip soundObject in _soundClips)
        {
            if(soundType == soundObject.soundType)
            {
                _audioSource.PlayOneShot(soundObject.clip);
            }
        }
    }

}

public enum SoundType
{
    EQUILIBRIUMBULLET,
    STATCOLLECT
}

[Serializable]
public class SoundClip
{
    public SoundType soundType;
    public AudioClip clip;
}