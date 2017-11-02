using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionScreen : MonoBehaviour {
	public GameObject mapGameObject;
	//private MapClic
	private MapClickableManager clickMan;

	public enum Option
	{
		Train,
		Breed,
		Catch,
		Fight
	}

	[SerializeField]
	public Option[] Clickables;

	void OnEnable() {
		clickMan = mapGameObject.GetComponent<MapClickableManager> ();
		//doStuff
	}




}
