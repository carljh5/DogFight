using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour {
	public GameObject[] panels;
	public TextAnim display;
	private string feedStr;
	private List<string> feedback = new List<string>();
	public bool isStreamRunning { get; private set; }
    public float waitingTime = 4f;
	private bool runStream;


	public void Feed(string str) {
		print (str);
		feedStr = str;
		runStream = true;
		if(!isStreamRunning)
			StartCoroutine (Stream());
		
	}

	public void StopFeed() {
		runStream = false;
	}

	public void FeedSingle(string str) {
		feedStr = str;
		StartCoroutine (SingleStream());
	}

	IEnumerator SingleStream() {
		foreach (GameObject go in panels) {
			go.SetActive (true);
			if(!go.name.Contains("Feedback"))
				go.SetActive (false);

		}

		display.Play (feedStr);
		yield return new WaitForSeconds (3f);

		foreach (GameObject go in panels) {
			if (go.name.Contains ("Action")) {
				go.SetActive (true);
			} else {
				go.SetActive (false);
			}
		}
	}


	IEnumerator Stream() {
		isStreamRunning = true;
		foreach (GameObject go in panels) {
			go.SetActive (true);
			if(!go.name.Contains("Feedback"))
				go.SetActive (false);

		}

		string tempStr = "";

		while (runStream) {
			if (tempStr != feedStr) {
				tempStr = feedStr;
				display.Play (tempStr);
			}
			yield return null;
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
