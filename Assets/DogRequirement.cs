using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DogRequirement : MonoBehaviour {

	public GameObject PopUp;

	[Serializable]
	public class ToggableObject {
		public GameObject gameObject;
		public bool setActive;
	}


	public ToggableObject[] gameObjectsToToggle;

	public void Click(BaseEventData data) {
		PointerEventData pointer = data as PointerEventData;
		if (GameManager.PlayerDogs.Count == 0) {
			PopUp.SetActive (true);
		} else {
			for (int i = 0; i < gameObjectsToToggle.Length; i++) {
				gameObjectsToToggle [i].gameObject.SetActive (gameObjectsToToggle[i].setActive);
			}
		}
	}

}
