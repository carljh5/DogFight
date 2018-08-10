using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateInputField : MonoBehaviour {
	InputField inputField;

    public List<GameObject> toToggle;

    public bool DogName;

    public Image DogImage;
    public Text RaceText;

    //TODO: add a confirm button, so players can type in no name and not any failuers.
	void OnEnable() {
		inputField = GetComponent<InputField> ();
		inputField.ActivateInputField ();
	    inputField.characterLimit = 24;
        // = Exterminator the Mad Dog

        var d = GameManager.PlayerDogs.FindLast(dog => true);

        DogImage.sprite =d.sprite;

        RaceText.text = d.GetGender() + " " + Dog.AsString(d.race);

        inputField.text = d.dogName;

        inputField.onEndEdit.AddListener(delegate
        {
            if (string.IsNullOrEmpty(inputField.text))
                return;

            if (DogName)
                GameManager.SetDogName(inputField.text);
            else
                GameManager.SetName(inputField.text);

            foreach (var go in toToggle)
            {
                go.SetActive(!go.activeSelf);
            }
        });
	}

}
