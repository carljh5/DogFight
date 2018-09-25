using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialExceptionScript : MonoBehaviour {

    public GameObject[] dojos;
    private bool isADojoActive;
    private static bool hasShowedTutorial;
    public GameObject tutorialToToggle;
    public GameObject otherThingToToggle;

    private List<EventTrigger.Entry> triggers = new List<EventTrigger.Entry>();

    private void OnEnable()
    {
        isADojoActive = false;
        foreach(GameObject go in dojos) {
            if (go.activeSelf)
                isADojoActive = true;
        }
        EventTrigger trigger = GetComponent<EventTrigger>();
        if (!hasShowedTutorial && isADojoActive) {
            triggers = trigger.triggers;
            trigger.triggers.Clear();
            EventTrigger.Entry newEntry = new EventTrigger.Entry();
            newEntry.callback.AddListener(HandleUnityAction);
            newEntry.eventID = EventTriggerType.PointerClick;
            trigger.triggers.Add(newEntry);
            hasShowedTutorial = true;
        } else if (hasShowedTutorial) {
            trigger.triggers.Clear();
            trigger.triggers = triggers;
        }
    }

    public void DoSomething()
    {
        isADojoActive = false;
        foreach (GameObject go in dojos)
        {
            if (go.activeSelf)
                isADojoActive = true;
        }

        if (!hasShowedTutorial && isADojoActive)
        {
            tutorialToToggle.SetActive(true);
            hasShowedTutorial = true;
        } else {
            otherThingToToggle.SetActive(true);
        }
    }

    public void HandleUnityAction(BaseEventData arg0)
    {

    }

}
