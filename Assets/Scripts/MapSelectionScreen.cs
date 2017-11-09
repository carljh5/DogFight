using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionScreen : MonoBehaviour {
	public GameObject mapGameObject;
	//private MapClic
	private MapClickableManager clickMan;

	/*public enum Option
	{
		Train,
		Breed,
		Catch,
		Fight,
		Story
	}*/

	public MapClickableManager.ClickableProperty[] clickableProperties;

	//[SerializeField]
	//public Option[] Clickables;

	void OnEnable() {
		clickMan = mapGameObject.GetComponent<MapClickableManager> ();
		clickMan.gameObject.SetActive (true);
		clickMan.SetScreen (clickableProperties);
		gameObject.SetActive (false);
	}




}
