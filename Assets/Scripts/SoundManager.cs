using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private List<SoundClip> _soundClips;

    private void OnEnable()
    {
        EventSystem.PlaySoundClip += HandlePlayingClip;
    }

    private void OnDisable()
    {
        EventSystem.PlaySoundClip -= HandlePlayingClip;
    }

    private void HandlePlayingClip(SoundType soundType)
    {
/*         if (_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return;
        } */

        foreach (SoundClip soundObject in _soundClips)
        {
            if (soundObject.clip == null)
            {
                Debug.LogError("AudioClip is null for sound type: " + soundObject.soundType);
                continue;
            }

            if (soundType == soundObject.soundType)
            {
                _audioSource.PlayOneShot(soundObject.clip);
            }
        }
    }

}

public enum SoundType
{
    EQUILIBRIUMBULLET,
    STATCOLLECT,
    MONEY_COLLECT,
    VICTORY,
    DEFEAT,
    NEW_LEVEL
}

[Serializable]
public class SoundClip
{
    public SoundType soundType;
    public AudioClip clip;
}