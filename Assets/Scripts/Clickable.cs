using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour {

	public Image icon;
	public EventTrigger eventTrigger;
	public Image confirmImg;
	public Text confirmText;

	public void SetClickable(Sprite icon, GameObject scene) {
		this.icon.sprite = icon;
		confirmImg.sprite = icon;
		confirmText.text = ConfirmTextFromSpriteName (icon.name);
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener ((eventData) => {
			scene.SetActive (true);
		});
		eventTrigger.triggers.Add (entry);
	}

	string ConfirmTextFromSpriteName(string spriteName) {
		print (spriteName);
		string customStr = "";
		switch (spriteName) {
		case "Disc_Cellar":
			customStr = "Go to the Colima Cartel Cellar, Virgencitas?";
			break;
		case "Disc_Junkyard":
			customStr = "Go to the Old Junkyard, Baja California?";
			break;
		case "Disc_Landfill":
			customStr = "Go to the Bordo Poniente landfill, Mexico City?";
			break;
		case "Disc_Market":
			customStr = "Go to a Local Market, Mexico City?";
			break;
		case "Disc_Pablo":
			customStr = "Go to somewhere in the Mexico City slum?";
			break;
		default:
			customStr = "Go to disc name doesnt exist";
			break;
		}
		return customStr;
	}


}
