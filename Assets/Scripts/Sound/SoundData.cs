using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class SoundData : ScriptableObject
{
    public string soundName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0f, 1f)]
    public float volumeVariation;

    [Range(-3f, 3f)]
    public float pitch = 1f;

    [Range(0f, 1f)]
    public float pitchVariation;

    public SoundType SoundCategory = SoundType.GAME_SFX;

    public enum SoundType
    {
        INTERFACE,
        GAME_SFX,
        MUSIC_MAIN,
        MUSIC_SCENARIO,
        AMBIENCE
    }

    public static void SetupAudioSource(AudioSource source, SoundData sound)
    {
        source.pitch = sound.pitch + Random.Range(-sound.pitchVariation, sound.pitchVariation);
        source.volume = sound.volume + Random.Range(-sound.volumeVariation, sound.volumeVariation);
        source.clip = sound.clip;
    }
}