using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightDropDown : Dropdown {

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        value = -1;

        base.OnPointerClick(eventData);
    }

}
