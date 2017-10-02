using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class FightTestScript : MonoBehaviour
{
    public Dog dog1, dog2;

    public FightManager fightManager;

    private bool fightStarted;

    void Start()
    {
        dog1.NameDog("Beauty");
        dog2.NameDog("Beast");
    }

	// Update is called once per frame
	void Update () {
        if(!Input.GetKeyDown(KeyCode.Space))
	        return;

	    if (!fightStarted)
	    {
	        Debug.Log(fightManager.StartFight(dog1, dog2));
	        fightStarted = true;
	    }
	    else
	    {
            fightManager.Round(FightManager.FightAction.Tackle);
	    }
	}
}
