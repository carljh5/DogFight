using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryComponent : MonoBehaviour {
	public string[] paragraphs; 
	public TextAnim display;
	public GameObject[] panelsToToggle;

	private int index = 0;

	void OnEnable() {
		Next ();
        SoundManager.SetBackgroundSound(SoundManager.BackgroundSound.Start);
    }


    //TODO: check for escape characters like "/playername" and replace it with the relevant from the game manager.
	public void Next() {
		
        SoundManager.PlayClick();
		if (index < paragraphs.Length) {
			if (display.Play (paragraphs [index])) 
				index++;
		} else {
			if(display.Play (paragraphs [index-1])) 
			{
				foreach (GameObject go in panelsToToggle) {
					go.SetActive (go.activeSelf ? false : true);
				}
				gameObject.SetActive (false);
			}
		}
	}
}
