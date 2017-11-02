using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryComponent : MonoBehaviour {
	public string[] paragraphs; 
	public TextAnim display;
	public GameObject[] panelsToToggle;
    public SoundManager.BackgroundSound BackgroundSound;

	private int index = 0;

	void OnEnable() {
        display.Clear();
		Next ();
        SoundManager.SetBackgroundSound(BackgroundSound);
    }

	public void Next() {
		
        SoundManager.PlayClick();
		if (index < paragraphs.Length) {
			if (display.Play (GameManager.Clean(paragraphs[index])))
				index++;
		} else {
			if(display.Play (GameManager.Clean(paragraphs [index-1]))) 
			{
				foreach (GameObject go in panelsToToggle) {
					go.SetActive (go.activeSelf ? false : true);
				}
				gameObject.SetActive (false);
                //Not really working :
			    index = 0;
                display.Clear();
			}
		}
	}
}
