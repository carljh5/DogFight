using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogAnim : MonoBehaviour {
	Vector3 origPos;
	Vector3 origScale;
    public Image DogImage;


    private List<GameObject> InstantiatedObjects = new List<GameObject>();

	public GameObject blood;

	public enum animType {
		Hit,
		Attack,
        Death
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
					InstantiatedObjects.Add( Instantiate (blood, blood.transform.position, Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 359))), blood.transform.parent));
				blood.SetActive (true);
				break;
			case animType.Attack:
				StartCoroutine (AttackRoutine ());
				break;
            case animType.Death:
		        StartCoroutine(DeathRoutine());
		        break;
			default:
				break;
		}
	}

    IEnumerator DeathRoutine()
    {
        float time = 0;
        var startPos = transform.position;
        var startColor = DogImage.color;
        var xColor = startColor;

        float runTime = 2f;

        while (time < runTime)
        {
            xColor.a = startColor.a * (1f - time / runTime);

            DogImage.color = xColor;
            
            transform.position = new Vector3(transform.position.x, startPos.y + 100* time / runTime, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

    }

	IEnumerator HitRoutine() {
		Vector2 rand;
		float time = 0;
		Handheld.Vibrate ();
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

    public void CleanUp()
    {
        blood.SetActive(false);

        foreach (var go in InstantiatedObjects)
        {
            Destroy(go);
        }
    }
}
