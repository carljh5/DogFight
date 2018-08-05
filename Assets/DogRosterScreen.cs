using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogRosterScreen : MonoBehaviour {

    public DogEntry Entry;
    
    private List<GameObject> instantiatedEntries = new List<GameObject>();
    
    public StatScreen Stats;
    
    void OnEnable()
    {
        foreach (var e in instantiatedEntries)
        {
            Destroy(e);
        }


        for (int i = 0; i < GameManager.PlayerDogs.Count; i++)
        {
            var d = GameManager.PlayerDogs[i];

            var obj = Instantiate(Entry, Entry.transform.parent);

            var e = obj.GetComponent<DogEntry>();

            e.Name.text = d.dogName + " " + (d.male ? "♂" : "♀");
            e.Kills.text = d.kills.ToString("0");
            e.DogImage.sprite = d.sprite;

            //e.Click = (() => Stats.ShowDog(i));

            instantiatedEntries.Add(obj.gameObject);
            
        }
        Entry.gameObject.SetActive(false);
        
    }
    
}
