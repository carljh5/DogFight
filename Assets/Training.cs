using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour {
	public Image fill;
	public float decay = 10f;
	public GameObject scene;
	float maxVal = 100f;
	float minVal = 0f;
	float curVal = 50f;

	Coroutine co;

	public void Train() {
		if(co == null) {
			co = StartCoroutine (TrainRoutine());
		}
		curVal += 5;
		Handheld.Vibrate ();
	}

	IEnumerator TrainRoutine() {
		while (true) {
			if(fill != null)
				fill.fillAmount = curVal / 100f;
			if (curVal >= maxVal) {
				gameObject.SetActive (false);
				scene.SetActive (true);
				break;
			}
			curVal -= decay * Time.deltaTime;
			yield return null;
			if (curVal <= minVal) {
				gameObject.SetActive (false);
				scene.SetActive (true);
				break;
			}
		}
		yield return null;
	}

}
