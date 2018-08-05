using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public Entry StatEntry;
    private Dog d;

    public Image DogImage;

    public Text NameText;

    public bool showEnemyDog;

    private List<GameObject> instantiatedEntries = new List<GameObject>();

    //UNUSED
    public Text RaceText;

    private Dictionary<string, float> stats = new Dictionary<string, float>();

    void OnEnable()
    {
        foreach (var e in instantiatedEntries)
        {
            Destroy(e);
        }

        stats["Aggression"] = d.aggression;
        stats["Strength"] = d.strength;
        stats["Courage"] = d.courage;
        stats["Speed"] = d.speed;
        stats["Bite"] = d.bite;
        stats["Dogs killed"] = d.kills;

	    foreach (var pair in stats)
	    {

            var obj = Instantiate(StatEntry.gameObject, StatEntry.transform.parent);

	        var e = obj.GetComponent<Entry>();

	        e.Stat.text = pair.Key;
	        e.Value.text = pair.Value.ToString("0");
            e.gameObject.SetActive(true);

            instantiatedEntries.Add(obj);
	    }
        StatEntry.gameObject.SetActive(false);

	    NameText.text = d.dogName + " " + (d.male ? "♂" : "♀");

        DogImage.sprite = d.sprite;
	}

    internal void ShowDog(int i)
    {
        d = GameManager.PlayerDogs[i];

        gameObject.SetActive(true);
    }

    public void ShowEnemyDog(bool show)
    {
        showEnemyDog = show;
        
        //could be fed to show enemy dogs
        d = showEnemyDog ? GameManager.GetNextEnemy() : GameManager.PlayerDog;
        
        gameObject.SetActive(true);
    }



    public void ShowLastDog()
    {
        d = GameManager.PlayerDogs[GameManager.PlayerDogs.Count-1];

        gameObject.SetActive(true);
    }
}

