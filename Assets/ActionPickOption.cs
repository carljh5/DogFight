using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class ActionPickOption : MonoBehaviour
{
    [Range(0, 10)]
    public float SecondsBeforeAutoPick;

    public GameObject[] panelsToDeactivate;

    public FightManager Manager;

    void OnEnable()
    {
        StartCoroutine(PickOptionRoutine());
    }

    IEnumerator PickOptionRoutine()
    {
        yield return new WaitForSeconds(SecondsBeforeAutoPick);

        foreach (var p in panelsToDeactivate)
        {
            p.SetActive(false);
        }

        Manager.Round(FightManager.FightAction.NoAction);
    }
}
