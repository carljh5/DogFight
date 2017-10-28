using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class FightTestScript : MonoBehaviour
{
    public Dog dog1, dog2;

    public FightManager fightManager;
    private bool aggressionRoundDone;
    private bool fightStarted;

    void Start()
    {
    }

	// Update is called once per frame
	void Update () {

        if (!fightStarted)
        {
            Debug.Log(fightManager.StartFight(dog1, dog2));
            fightStarted = true;
        }

        if (!Input.GetKeyDown(KeyCode.Space))
	        return;

	    if (!aggressionRoundDone)
	    {
	        Debug.Log(fightManager.AggressionRound());
	        aggressionRoundDone = true;
	    }
	    else
	    {
            var rnd = new System.Random();
	        fightManager.Round((FightManager.FightAction)
	            Enum.GetValues(typeof(FightManager.FightAction)).GetValue(rnd.Next((int) FightManager.FightAction.Count)));
	    }
	}
}
