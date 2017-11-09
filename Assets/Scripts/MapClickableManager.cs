using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapClickableManager : MonoBehaviour {
	[SerializeField]
	private Clickable clickable;
	private List<Clickable> clickables = new List<Clickable>();

	public enum Option
	{
		Train,
		Breed,
		Catch,
		Fight,
		Story
	}

	[Serializable]
	public class ClickableProperty {
		public Sprite icon;
		public Option option;
		public GameObject scene;
	}

	public ClickableProperty[] clickableDefaults;

	public void SetScreen(ClickableProperty[] clickableProperties) {

		foreach (Clickable click in clickables) {
			Destroy (click.gameObject);
		}

		foreach(ClickableProperty clickProp in clickableProperties) {
			Clickable clone = Instantiate (clickable, clickable.transform.parent);
			/*Clickable clone = Instantiate (clickable, 
				new Vector3 (UnityEngine.Random.Range (50f, Screen.height-400f), UnityEngine.Random.Range (50, Screen.width-300f)), 
					Quaternion.identity, clickable.transform.parent);*/
			clickables.Add (clone);

			Sprite tempSpr = null;
			GameObject tempScene = null;

			foreach(ClickableProperty prop in clickableDefaults) {
				if (prop.option.Equals (clickProp.option)) {
					tempSpr = prop.icon;
					tempScene = prop.scene;
					break;
				}
			}

			tempSpr = clickProp.icon == null ? tempSpr : clickProp.icon;
			tempScene = clickProp.scene == null ? tempScene : clickProp.scene;



			clone.SetClickable (tempSpr,tempScene);
		}
		StartCoroutine (SpawnRoutine());
	}

	IEnumerator SpawnRoutine() { 
		RectTransform clickableRT = clickable.GetComponent<RectTransform> ();
		float clickableWidth = clickableRT.rect.width;
		float clickableHeight = clickableRT.rect.height;
		float padding = 20f;
	    var screenSize = GetComponentInParent<CanvasScaler>().referenceResolution;

		int divX = Mathf.FloorToInt(screenSize.y / (clickableWidth));
		int divY = Mathf.FloorToInt (screenSize.x/(clickableHeight));
		List<Vector3> positions = new List<Vector3>();
		for (int x = 0; x < divX; x++) {
			for (int y = 0; y < divY; y++) {
				positions.Add (new Vector3((x*clickableWidth)+padding, (y*clickableHeight)+padding));
			}
		}

		foreach(Clickable click in clickables) {
			int i = UnityEngine.Random.Range (0,positions.Count);
			click.transform.position = positions [i];
			positions.RemoveAt (i);
			click.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.5f);
		}
		yield return null;
	}

}
