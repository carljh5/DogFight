using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSeletion : MonoBehaviour
{
    public EventOption OptionEntry;
    public GameObject optionGameObject;
    public Text QuestionTextComponent;

    [Serializable]
    public struct Option
    {
        public string text;
        public GameManager.GameEvent optionEvent;
        public Sprite image;
    }
    public Option[] options;

    public string question;

    public void Awake()
    {
        optionGameObject.SetActive(false);
    }
    
    public void ShowOptions( )
    {
        optionGameObject.SetActive(true);

        QuestionTextComponent.text = question;

        foreach (var opt in options)
        {
            var obj = Instantiate(OptionEntry.gameObject,OptionEntry.transform.parent);

            obj.SetActive(true);

            obj.GetComponentInChildren<Text>().text = opt.text;

            if(opt.image != null)
                obj.GetComponentInChildren<Image>().sprite = opt.image;
            else
                obj.GetComponentInChildren<Image>().gameObject.SetActive(false);
            
            var ev = obj.GetComponent<EventOption>();

            ev.Event = opt.optionEvent;
        }
    }

}
