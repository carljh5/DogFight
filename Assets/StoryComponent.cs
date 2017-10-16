﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryComponent : MonoBehaviour {
	public string[] paragraphs; 
	public TextAnim display;
	public GameObject panelToGoTo;

	private int index = 0;

	void OnEnable() {
		Next ();
        SoundManager.SetBackgroundSound(SoundManager.BackgroundSound.Start);
    }

	public void Next() {
        SoundManager.PlayClick();
		if (index < paragraphs.Length) {
			if (display.Play (paragraphs [index]))
				index++;
		} else {
			if(display.Play (paragraphs [index-1])) {
				panelToGoTo.SetActive (true);
				gameObject.SetActive (false);
			}
		}
	}
}
