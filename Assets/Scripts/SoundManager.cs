using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] bites, whines, barks, death;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public float PlayBite()
    {
        var x = (int)(bites.Length * Random.value);

         audioSource.PlayOneShot(bites[x]);

        return bites[x].length;
    }

    public float PlayWhine()
    {
        var x = (int)(whines.Length * Random.value);

        audioSource.PlayOneShot(whines[x]);
        return whines[x].length;
    }

    public float PlayBark()
    {
        var x = (int)(barks.Length * Random.value);

        audioSource.PlayOneShot(barks[x]);
        return barks[x].length;
    }



}
