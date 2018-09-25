using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEndAnimation : MonoBehaviour {

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
