using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnim : MonoBehaviour {


	public float speedOfTyping = 0.05f;
	private Coroutine co;
	private bool isAnimPlaying;
	private string bufString = "";
	private Text text;

	void Start () {
		text = GetComponent<Text> ();	
	}

	void OnEnable() {
		text = GetComponent<Text> ();
	}

	void OnDisable() {
	    if (co != null)
	    {
	        StopCoroutine (co);
            SoundManager.StopTextSound();
	    }
	}

	public bool Play(string str) {
		if (!isAnimPlaying) {
			bufString = str;
			co = StartCoroutine (StringAnim (bufString));
			return true;
		} else {
			if(co != null)
				StopCoroutine (co);
			text.text = bufString;
            SoundManager.StopTextSound();
			isAnimPlaying = false;
			return false;
		}
	}


	IEnumerator StringAnim(string str) {
		isAnimPlaying = true;
		int charCount = str.Length;
		int index = 0;
		if(text == null)
			text = GetComponent<Text> ();
		if (text != null) {
            SoundManager.StartTextSound();
			text.text = "";
			while (text.text.Length < charCount) {
				yield return new WaitForSeconds (speedOfTyping);
				text.text += str.Substring (index, 1);
				index++;
			}
		}
        SoundManager.StopTextSound();
		isAnimPlaying = false;
		yield return null;
	}
}
