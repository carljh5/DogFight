using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSelection : MonoBehaviour {

    public Dropdown DogSelectDropDown;

    private void OnEnable()
    {

        var drops = GameManager.PlayerDogs.Select(d => new Dropdown.OptionData(d.dogName, d.sprite)).ToList();

        DogSelectDropDown.options.Clear();

        DogSelectDropDown.AddOptions(drops);

    }
}
