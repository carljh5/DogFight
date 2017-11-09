using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public Entry StatEntry;

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

        //could be fed to show enemy dogs
        var d = showEnemyDog ? GameManager.GetNextEnemy() : GameManager.PlayerDog;

        stats["Aggression"] = d.aggression;
        stats["Strength"] = d.strength;
        stats["Courage"] = d.courage;
        stats["Speed"] = d.speed;
        stats["Bite"] = d.bite;

	    foreach (var pair in stats)
	    {

            var obj = Instantiate(StatEntry.gameObject, StatEntry.transform.parent);

	        var e = obj.GetComponent<Entry>();

	        e.Stat.text = pair.Key;
	        e.Value.text = pair.Value.ToString("N");
            e.gameObject.SetActive(true);

            instantiatedEntries.Add(obj);
	    }
        StatEntry.gameObject.SetActive(false);

	    NameText.text = d.dogName;
	    DogImage.sprite = d.sprite;
	}

    public void ShowEnemyDog(bool show)
    {
        showEnemyDog = show;

        gameObject.SetActive(true);
    }
}
