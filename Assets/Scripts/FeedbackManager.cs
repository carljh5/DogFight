using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour {
	public GameObject[] panels;
	public TextAnim display;
	private List<string> feedback = new List<string>();
	public bool isStreamRunning { get; private set; }

	public void Feed(string str) {
		print (str);
		feedback.Add (str);
		if(!isStreamRunning)
			StartCoroutine (Stream());
	}

	IEnumerator Stream() {
		isStreamRunning = true;
		foreach (GameObject go in panels) {
			go.SetActive (true);
			if(!go.name.Contains("Feedback"))
				go.SetActive (false);

		}
		while (feedback.Count > 0) {
			display.Play (feedback [0]);
			yield return new WaitForSeconds (4f);
			feedback.Remove (feedback[0]);
		}
		foreach (GameObject go in panels) {
			if (go.name.Contains ("Action")) {
				go.SetActive (true);
			} else {
				go.SetActive (false);
			}
		}
		isStreamRunning = false;
	}
}
