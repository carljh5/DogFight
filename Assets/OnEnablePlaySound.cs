using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnablePlaySound : MonoBehaviour
{
    public AudioClip sound;
    
    void OnEnable()
    {
        if (sound == null)
        {
            Debug.LogWarning("No sound selected!");
        }
        else
            SoundManager.PlaySound(sound);
	}
}
