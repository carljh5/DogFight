using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour {

	public Image icon;
	public EventTrigger eventTrigger;

	public void SetClickable(Sprite icon, GameObject scene) {
		this.icon.sprite = icon;
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener ((eventData) => {
			scene.SetActive (true);
		});
		eventTrigger.triggers.Add (entry);
	}

	/*public void SetClickable(MapSelectionScreen.Option option) {
		switch (option) {
		case MapSelectionScreen.Option.Catch:
			break;
		case MapSelectionScreen.Option.Breed:
			break;
		case MapSelectionScreen.Option.Fight:
			break;
		case MapSelectionScreen.Option.Train:
			break;
		default:
			break;
		}
	}*/



}
