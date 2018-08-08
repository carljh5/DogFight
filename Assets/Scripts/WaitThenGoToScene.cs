using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenGoToScene : MonoBehaviour
{
    public int SecondsToWait = 4;
    public GameObject Scene;

	void OnEnable ()
	{
	    StartCoroutine(Routine());
	}

    IEnumerator Routine()
    {
        yield return new WaitForSeconds(SecondsToWait);
        //Scene.SetActive(true); We are not using this anymore. but the script is still usable for timed disabling
        gameObject.SetActive(false);
    }
}
