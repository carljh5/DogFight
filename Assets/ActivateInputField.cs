using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateInputField : MonoBehaviour {
	InputField inputField;
	void OnEnable() {
		inputField = GetComponent<InputField> ();
		inputField.ActivateInputField ();
	}
}
