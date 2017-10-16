using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBtn : MonoBehaviour {
	public FightManager.FightAction fightAction;
	private FightManager fightManager;
	private FeedbackManager feedback;
	private static bool aggressionRoundDone;

	public void Start() {
		fightManager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<FightManager> ();
		feedback = GameObject.FindGameObjectWithTag ("Manager").GetComponent<FeedbackManager> ();
	}

	public void Action() {
		fightManager.Round (fightAction);
	}
}
