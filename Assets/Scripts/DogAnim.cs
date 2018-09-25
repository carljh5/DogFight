using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogAnim : MonoBehaviour {
	Vector3 origPos;
	Vector3 origScale;
    public Image DogImage;
    public Image DogShadowImage;
    public Image BloodAura;

    private List<GameObject> InstantiatedObjects = new List<GameObject>();

	public GameObject blood;
    private Color startColor;
    private Color BloodColor;

    public enum animType {
		Hit,
		Attack,
        Death,
        Locking,
        Locked
    }

	void Start () {
		origPos = transform.position;
		origScale = transform.localScale;

        BloodColor = blood.GetComponent<Image>().color;
    }

	public void Play(animType anim) {
		switch (anim) {
		case animType.Hit:
				StartCoroutine (HitRoutine ());
                if (blood.activeSelf)
					InstantiatedObjects.Add( Instantiate (blood, blood.transform.position, Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 359))), blood.transform.parent));
                blood.SetActive(true);

                break;
			case animType.Attack:
				StartCoroutine (AttackRoutine ());
				break;
            case animType.Death:
		        StartCoroutine(DeathRoutine());
		        break;
            case animType.Locking:
                DogShadowImage.gameObject.SetActive(true);
                break;
            case animType.Locked:
                BloodAura.gameObject.SetActive(true);
                break;
			default:
				break;
		}
	}

    IEnumerator DeathRoutine()
    {
        float time = 0;

        List<Image> bloodImages = new List<Image>();

        var origBloodImage = blood.GetComponent<Image>();

        bloodImages.Add(origBloodImage);

        foreach(var go in InstantiatedObjects)
        {
            var im = go.GetComponent<Image>();

            if (im)
                bloodImages.Add(im);
        }


        startColor = DogImage.color;
        var xColor = startColor;

        float runTime = 2f;

        while (time < runTime)
        {
            xColor.a = startColor.a * (1f - time / runTime);

            DogImage.color = xColor;

            foreach (Image im in bloodImages)
            {
                im.color = xColor;
            }
            transform.position = new Vector3(transform.position.x, origPos.y + 100* time / runTime, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

        //Hack to make sure the position is reset
        yield return new WaitForSeconds(6);
        transform.position = origPos;
    }

	IEnumerator HitRoutine() {
		Vector2 rand;
		float time = 0;
#if UNITY_IOS
        Handheld.Vibrate ();
#endif
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
        blood.GetComponent<Image>().color = BloodColor;

        blood.SetActive(false);

        DogImage.color = Color.white;
        transform.position = origPos;

        foreach (var go in InstantiatedObjects)
        {
            Destroy(go);
        }
    }
}
