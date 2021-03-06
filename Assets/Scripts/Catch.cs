﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catch: MonoBehaviour {
	public RectTransform bar;
	public RectTransform target;
	public RectTransform marker;
	public float speed = 900;
    public Animator LassoAnimator;
    public GameObject FailScene;
    public GameObject SuccessScene;
    public List<Dog> CatchDogs;
    private Dog dogToCatch;
    public Image DogImage;


	Coroutine co;
    private bool isCatchAble;

    void OnEnable() {
        dogToCatch = CatchDogs[Random.Range(0, CatchDogs.Count - 1)];
        DogImage.sprite = dogToCatch.sprite;
        speed =  200 + 45* (dogToCatch.speed + dogToCatch.aggression + dogToCatch.strength + dogToCatch.bite);
        Debug.Log("speed: " + speed);

		co = StartCoroutine (MarkerRoutine());
	}

	public void Throw() {
		if(co!=null)
			StopCoroutine (co);

        if (isCatchAble)
        {
            Debug.Log("caught the " + dogToCatch.race);
            StartCoroutine(SwitchToSuccesScene());
        }
        else
            StartCoroutine(SwitchToFailScene());
	}

    public IEnumerator SwitchToFailScene()
    {
        LassoAnimator.Play("LassoThrow");
        SoundManager.PlayThrow();
        yield return new WaitForSeconds(1);

        FailScene.SetActive(true);
        gameObject.SetActive(false);
    }


    public IEnumerator SwitchToSuccesScene()
    {
        LassoAnimator.Play("LassoThrow");
        SoundManager.PlayThrow();
        yield return new WaitForSeconds(1);
        SoundManager.PlayCatch();

        //Remove dog from catchables
        CatchDogs.Remove(dogToCatch);

        //add dog to players roster
        GameManager.PlayerDogs.Add(dogToCatch);

        SuccessScene.SetActive(true);
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
