using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnim : MonoBehaviour {
	Vector3 origPos;
	Vector3 origScale;

	public GameObject blood;

	public enum animType {
		Hit,
		Attack
	}

	void Start () {
		origPos = transform.position;
		origScale = transform.localScale;
	}

	public void Play(animType anim) {
		switch (anim) {
		case animType.Hit:
				StartCoroutine (HitRoutine ());
				if (blood.activeSelf)
					Instantiate (blood, blood.transform.position, Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 359))), blood.transform.parent);
				blood.SetActive (true);
				break;
			case animType.Attack:
				StartCoroutine (AttackRoutine ());
				break;
			default:
				break;
		}
	}

	public void Play() {
		StartCoroutine (HitRoutine());
		if (blood.activeSelf)
			Instantiate (blood, blood.transform.position, Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 359))), blood.transform.parent);
		blood.SetActive (true);
	}

	IEnumerator HitRoutine() {
		Vector2 rand;
		float time = 0;
		while (time < 0.7f) {
			rand = Random.insideUnitCircle * 10f;
			transform.position = new Vector3 (origPos.x+rand.x, origPos.y+rand.y, origPos.z);
			time += Time.deltaTime;
			yield return null;
		}
		transform.position = origPos;
	}

	IEnumerator AttackRoutine() {
		float time = 0;
		while (time < 0.3f) {
			float scale = Mathf.Lerp (origScale.y, origScale.y * 1.5f, (1f * time) / 0.3f);
			transform.localScale = new Vector3(scale, scale, origScale.z);
			time += Time.deltaTime;
			yield return null;
		}
		transform.localScale = origScale;
	}
}
