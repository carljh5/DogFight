using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnim : MonoBehaviour {

	string firsString = "Another day in the Mexico City slum. Your head aches and your throat is sore. " +
		"Your stomach ache reminds you of your unsatisfied hunger...";
	Text text;

	float speedOfTyping = 0.05f;

	Coroutine co;

	void Start () {
		text = GetComponent<Text> ();
	}
	
	public void Next() {
		if (co == null) {
			co = StartCoroutine (StringAnim (firsString));
		} else {
			StopCoroutine (co);
			text.text = firsString;
		}
	}


	IEnumerator StringAnim(string str) {
		int charCount = str.Length;
		int index = 0;
		while (text.text.Length < charCount) {
			yield return new WaitForSeconds (speedOfTyping);
			text.text += str.Substring (index, 1);
			index++;
		}
	}

	void Update () {
		
	}
}
