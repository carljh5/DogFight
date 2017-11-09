using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch: MonoBehaviour {
	public RectTransform bar;
	public RectTransform target;
	public RectTransform marker;
	public float speed = 20f;
	public bool isCatchAble;
    public Animator LassoAnimator;
    public GameObject FailScene;

	Coroutine co;

	void OnEnable() {
		co = StartCoroutine (MarkerRoutine());
	}

	public void Throw() {
		if(co!=null)
			StopCoroutine (co);
        
        LassoAnimator.Play("LassoThrow");
	    StartCoroutine(SwitchToScene());
	}

    public IEnumerator SwitchToScene()
    {
        SoundManager.PlayThrow();
        yield return new WaitForSeconds(1);

        FailScene.SetActive(true);
        gameObject.SetActive(false);
    }

	IEnumerator MarkerRoutine() {
		float barSizeX = bar.rect.size.x/2f;
		float targetSizeX = target.rect.size.x / 2f;
		Vector2 dir = Vector2.right;
		while (true) {
			if(marker.anchoredPosition.x >= bar.anchoredPosition.x+barSizeX) {
				dir = Vector2.left;
			} else if (marker.anchoredPosition.x <= bar.anchoredPosition.x-barSizeX) {
				dir = Vector2.right;
			}
			if (marker.anchoredPosition.x <= target.anchoredPosition.x + targetSizeX && marker.anchoredPosition.x >= target.anchoredPosition.x - targetSizeX) {
				isCatchAble = true;
			} else {
				isCatchAble = false;
			}
			marker.anchoredPosition += dir*Time.deltaTime*speed;
			yield return null;
		}
	}
}
