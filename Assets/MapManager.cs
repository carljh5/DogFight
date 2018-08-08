using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	public GameObject[] Dojos;
	private int amountOfDojos;
	private int lastDojoIndex = 0;

	// Use this for initialization
	void Start () {
		amountOfDojos = Dojos.Length;
	}

	public void ToggleNextDojo() {
		lastDojoIndex = lastDojoIndex + 1 % amountOfDojos;
		Dojos [lastDojoIndex].gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.A)) {
//			ToggleNextDojo ();
//		}
//		
	}
}
