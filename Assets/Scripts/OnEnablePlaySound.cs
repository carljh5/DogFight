using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnablePlaySound : MonoBehaviour
{
    public AudioClip sound;
    public int delay;
    
    void OnEnable()
    {
        if (sound == null)
        {
            Debug.LogWarning("No sound selected!");
        }
        else
            StartCoroutine(PlayDelayed());
    }

    IEnumerator PlayDelayed()
    {
        yield return new WaitForSeconds(delay);
        SoundManager.PlaySound(sound);
    }
}
