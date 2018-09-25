using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnDisableScript : MonoBehaviour {

    public GameObject tutorialToToggle;
    private bool hasBeenToggled;

    private void OnDisable()
    {
        if(!hasBeenToggled) {
            tutorialToToggle.SetActive(true);
            hasBeenToggled = true;
        }
    }
}
