using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventOption : MonoBehaviour
{
    public GameObject OptionPanel;

    [HideInInspector]
    public GameManager.GameEvent Event { get; internal set; }

    [HideInInspector]
    public GameObject Scene, PreviousScene;

    public void SelectThis()
    {
        if (Scene != null)
        {
            Scene.SetActive(true);
            PreviousScene.SetActive(false);
        }
        else
            GameManager.OptionSelected(Event);

        OptionPanel.SetActive(false);
    }
}
