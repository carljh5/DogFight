using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			Clickable clone = Instantiate (clickable, 
				new Vector3 (UnityEngine.Random.Range (50f, Screen.height-400f), UnityEngine.Random.Range (50, Screen.width-300f)), 
					Quaternion.identity, clickable.transform.parent);
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
		foreach(Clickable click in clickables) {
			click.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.5f);
		}
		yield return null;
	}

}
