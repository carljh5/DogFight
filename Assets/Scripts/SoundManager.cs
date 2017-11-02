using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO: make all public methods static
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Header("Fight sounds")]
    public AudioClip[] bites;
    public AudioClip[] whines;
    public AudioClip[] barks;
    public AudioClip[] death;

    [Header("UI sounds")]
    public AudioClip popUp;
    public AudioClip textAudio;
    public float textVolume = 0.05f;
    public AudioClip clickSound;

    [Header("Background sounds")]
    public AudioClip fightBackground;
    public AudioClip startBackground;
    public AudioClip streetBackground0;
    public AudioClip streetBackground1;

    private BackgroundSound currentBackground;

    public Dictionary<BackgroundSound,AudioClip> backgroundSounds;

    public enum BackgroundSound
    {
        NoSound,
        Start,
        Street0,
        Street1,
        Fight0
    }

    [Header("Audio Sources")]
    public AudioSource TextAudioSource;
    public AudioSource BackgroundAudioSource;
    public AudioSource SfxAudioSource;


    void Awake()
    {
        instance = this;

        backgroundSounds = new Dictionary<BackgroundSound, AudioClip>();

        backgroundSounds.Add(BackgroundSound.Start, startBackground);
        backgroundSounds.Add(BackgroundSound.Street0, streetBackground0);
        backgroundSounds.Add(BackgroundSound.Street1, streetBackground1);
        backgroundSounds.Add(BackgroundSound.Fight0, fightBackground);
    }

    public static void StartTextSound()
    {
        instance.startTextSound();
    }

    public static void StopTextSound()
    {
        instance.stopTextSound();
    }

    public static void SetBackgroundSound(BackgroundSound background, float volume = 0.05f)
    {
        if (instance.currentBackground == background)
            return;

        instance.currentBackground = background;

        instance.BackgroundAudioSource.volume = volume;

        instance.BackgroundAudioSource.clip = instance.backgroundSounds[background];

        instance.BackgroundAudioSource.Play();
    }

    public static BackgroundSound GetBackgroundSound()
    {
        return instance.currentBackground;
    }

    private void startTextSound()
    {
        TextAudioSource.clip = textAudio;
        TextAudioSource.loop = true;
        TextAudioSource.volume = textVolume;
        TextAudioSource.Play();
    }

    private void stopTextSound()
    {
        if (TextAudioSource.clip == textAudio)
        {
            TextAudioSource.clip = null;
            TextAudioSource.Stop();
            TextAudioSource.loop = false;
        }

    }

    public static void PlayClick()
    {
        instance.SfxAudioSource.PlayOneShot(instance.clickSound,0.3f);
    }

    public static void PlayPopUp()
    {
        instance.SfxAudioSource.PlayOneShot(instance.popUp);
    }

    public float  PlayBite()
    {
        var x = (int)(bites.Length * Random.value);

        SfxAudioSource.PlayOneShot(bites[x]);

        return bites[x].length;
    }

    public float PlayWhine()
    {
        var x = (int)(whines.Length * Random.value);

        SfxAudioSource.PlayOneShot(whines[x]);
        return whines[x].length;
    }

    public float PlayBark()
    {
        var x = (int)(barks.Length * Random.value);

        SfxAudioSource.PlayOneShot(barks[x]);
        return barks[x].length;
    }
}
