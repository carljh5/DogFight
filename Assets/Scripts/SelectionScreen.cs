using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public EventOption OptionEntry;
    public GameObject optionGameObject;
    public Text QuestionTextComponent;
    protected static List<GameObject> instantiatedEntries  = new List<GameObject>();
    

    public string question;

    public void Awake()
    {
        optionGameObject.SetActive(false);
    }

    public virtual void ShowOptions()
    { }
}
