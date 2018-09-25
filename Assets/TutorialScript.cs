using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {
    private StoryComponent storyComponent;
    public GameObject tutorialObjectToToggle;
    private bool hasBeenToggled;

    private void OnEnable()
    {
        if(storyComponent == null)
            storyComponent = GetComponent<StoryComponent>();
        if (!hasBeenToggled)
        {
            storyComponent.AddPanel(tutorialObjectToToggle);
            hasBeenToggled = true;
        } else {
            storyComponent.RemovePanel(tutorialObjectToToggle);
        }
    }

   
	
	
}
