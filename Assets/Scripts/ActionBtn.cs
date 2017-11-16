using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBtn : MonoBehaviour {
	public FightManager.FightAction fightAction;
	private FightManager fightManager;

	public void Start() {
		fightManager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<FightManager> ();
	}

	public void Action() {
		fightManager.Round (fightAction);
	}
}
