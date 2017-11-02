using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateInputField : MonoBehaviour {
	InputField inputField;

    public List<GameObject> toToggle;

    public bool DogName;

    //TODO: add a confirm button, so players can type in no name and not any failuers.
	void OnEnable() {
		inputField = GetComponent<InputField> ();
		inputField.ActivateInputField ();
        
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
