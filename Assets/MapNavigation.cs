using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapNavigation : MonoBehaviour {
	private RectTransform panelRectTransform;
	private RectTransform parentRectTransform;
	// Use this for initialization
	void Start () {
		panelRectTransform = transform.GetComponent<RectTransform> ();
		parentRectTransform = transform.parent.GetComponent<RectTransform> ();
	}

	public void Drag(BaseEventData data) {
		PointerEventData pointer = data as PointerEventData;

		panelRectTransform.anchoredPosition += pointer.delta;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
