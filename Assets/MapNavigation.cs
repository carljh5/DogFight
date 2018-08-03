using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapNavigation : MonoBehaviour {
	private RectTransform RT;
	// Use this for initialization
	void Start () {
		RT = transform.GetComponent<RectTransform> ();
	}

	public void Drag(BaseEventData data) {
		PointerEventData pointer = data as PointerEventData;
		float radiusX = RT.rect.width/2f;
		float radiusY = RT.rect.height/2f;
		print (" x position rect " + RT.anchoredPosition.x + "    x position of right bounds " + (RT.anchoredPosition.x + radiusX));
		if ((RT.anchoredPosition.x + radiusX) < Screen.width)
			print ("STOP");


		RT.anchoredPosition += pointer.delta;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
