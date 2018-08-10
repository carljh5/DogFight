using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickUpDayOnEnable : MonoBehaviour {

    public void OnEnable()
    {
        GameManager.NextDay();
    }

    private void OnDisable()
    {
        GameManager.CheckMoney();
    }
}
