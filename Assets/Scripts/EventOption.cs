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
            PreviousScene.SetActive(false);
            Scene.SetActive(true);
            
        }

        GameManager.OptionSelected(Event);
        GameManager.SetPanelActive(true);

        if(Scene.GetComponent<SceneSelection>() == null )
            OptionPanel.SetActive(false);
    }
}
