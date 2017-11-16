using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelection : SelectionScreen
{

    [Serializable]
    public struct Option
    {
        public string text;
        //the scene to be displayed when next is selected
        public GameObject scene;
        public Sprite image;
    }
    public Option[] options;



	void OnEnable() {
		if (playOnAwake)
			ShowOptions ();

    }
    
    void OnDisable()
    {
        print("Enabling panel;");
        GameManager.SetPanelActive(true);
    }


    public override void ShowOptions( )
    {

        print("Disabling panel;");
        GameManager.SetPanelActive(false);

        //DELETE OLD ENTRIES
        foreach (var entry in instantiatedEntries)
        {
            Destroy(entry);
        }

        optionGameObject.SetActive(true);

        QuestionTextComponent.text = GameManager.Clean(question);

        foreach (var opt in options)
        {
            var obj = Instantiate(OptionEntry.gameObject,OptionEntry.transform.parent);

            obj.SetActive(true);

            obj.GetComponentInChildren<Text>().text = GameManager.Clean(opt.text);

            if (opt.image != null)
                obj.GetComponentInChildren<Image>().sprite = opt.image;
            else
                obj.GetComponentInChildren<Image>().gameObject.SetActive(false);

            var ev = obj.GetComponent<EventOption>();

            ev.Scene = opt.scene;
            ev.PreviousScene = this.gameObject;

            instantiatedEntries.Add(obj);
        }
    }
}
