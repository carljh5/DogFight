using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryComponent : MonoBehaviour {
	public string[] paragraphs; 
	public TextAnim display;
	public GameObject[] panelsToToggle;
    public SoundManager.BackgroundSound BackgroundSound;
	public bool playOnStart = false;

	private int index = 0;

	void Start() {
		if (playOnStart) {
			Play ();
			playOnStart = false;
		}
	}

	void OnEnable() {
		if(!playOnStart)
			Play ();
    }

	void Play() {
		display.Clear();
		Next ();
		SoundManager.SetBackgroundSound(BackgroundSound);
	}

    public void AddPanel(GameObject go) {
        GameObject[] temp = panelsToToggle;
        panelsToToggle = new GameObject[panelsToToggle.Length+1];
        for (int i = 0; i < temp.Length; i++) {
            panelsToToggle[i] = temp[i];
        }
        panelsToToggle[panelsToToggle.Length - 1] = go;

    }

    public void RemovePanel(GameObject go) {
        GameObject[] temp = panelsToToggle;
        panelsToToggle = new GameObject[panelsToToggle.Length -1];
        for (int i = 0; i < panelsToToggle.Length; i++) {
            if (temp[i] != go)
                panelsToToggle[i] = temp[i];
        }
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
