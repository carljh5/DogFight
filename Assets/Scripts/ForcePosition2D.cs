using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePosition2D : MonoBehaviour {
	//RectTransform rectTransform;


	void Start() {
		//rectTransform = GetComponent<RectTransform> ();
	}

	public enum align
	{
		center
	}

	public align alignment;

	void Update () {
		switch(alignment) {
		case align.center:
			transform.position = new Vector3 (Screen.width / 2f, Screen.height/2f);
			break;
		}
	}
}
