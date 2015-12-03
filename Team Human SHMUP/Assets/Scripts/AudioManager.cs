using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    public AudioSource audioSource;

    public AudioClip BossDeathAudio;
    public AudioClip BossEnterAudio;
    public AudioClip ClickButtonAudio;
    public AudioClip ExplosionAudio;
    public AudioClip HitAudio;
    public AudioClip LazerFireAudio;
    public AudioClip LevelCompleteAudio;
    public AudioClip PickUpAudio;
    public AudioClip RocketFireAudio;
    public AudioClip ShipEngineAudio;
    public AudioClip SpaceBGMusic;

    //Handles all the audio clips
    public void PlaySound(AudioSource source, AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
    }

    public void SpaceBGMusicSound()
    {
        PlaySound(audioSource, SpaceBGMusic, 0.1f);
    }

    public void BossDeathSound()
    {
        PlaySound(audioSource, BossDeathAudio, 0.2f); //
    }

    public void BossEnterSound()
    {
        PlaySound(audioSource, BossEnterAudio, 0.25f); //
    }

    public void ClickButtonSound()
    {
        PlaySound(audioSource, ClickButtonAudio, 5.0f);
    }

    public void ExplosionAudioSound()
    {
        PlaySound(audioSource, ExplosionAudio, 3.0f);
    }

    public void HitAudioSound()
    {
        PlaySound(audioSource, HitAudio, 3.0f);
    }

    public void LazerFireAudioSound()
    {
        PlaySound(audioSource, LazerFireAudio, 0.25f);
    }

    public void LevelCompleteAudioSound()
    {
        PlaySound(audioSource, LevelCompleteAudio, 0.25f); //
    }

    public void PickUpAudioSound()
    {
        PlaySound(audioSource, PickUpAudio, 0.4f); //
    }

    public void RocketFireAudioSound()
    {
        PlaySound(audioSource, RocketFireAudio, 0.8f);
    }

    public void ShipEngineAudioSound()
    {
        PlaySound(audioSource, ShipEngineAudio, 0.1f);
    }
}

