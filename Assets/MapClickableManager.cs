using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClickableManager : MonoBehaviour {
	[SerializeField]
	private Clickable clickable;
	private List<GameObject> clickables = new List<GameObject>();

	//public class icons


	public void SetScreen(MapSelectionScreen.Option[] clickableOptions) {
		foreach(MapSelectionScreen.Option opt in clickableOptions) {
			Clickable clone = Instantiate (clickable, clickable.transform.parent);

		}
		StartCoroutine (SpawnRoutine(clickableOptions));
	}

	IEnumerator SpawnRoutine(MapSelectionScreen.Option[] clickableOptions) {
		yield return null;
	}

}
