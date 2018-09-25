using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignImageOnEnable : MonoBehaviour {

    public Image dogImage;

    private void OnEnable()
    {
        GetComponent<Image>().sprite = dogImage.sprite;
    }
}
