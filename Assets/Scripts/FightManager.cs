using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    private bool fightRunning;

    public Dog dog1, dog2;

    private DogAnim dog1Anim, dog2Anim;

    public Image dog1HealthBar, dog2HealthBar;

    public Text Dog1Text, Dog2Text;

    public Image Dog1Image, Dog2Image;

    private static readonly float waitSeconds = 4f;

    private static readonly float biteLockStrengthDecrease = 0.5f;

    public SoundManager sound;

	public string feedbackStr = "";

	public FeedbackManager feedback;

    public GameObject[] ToggleAfterMatch;

    public enum FightAction
    {
        ThroatBite,
        LockBite,
        Tackle,
        Scratch,
        UseItem,
        SwitchDog,
        Run,



        Count
    }

    public void StartFight()
    {
        dog1 = GameManager.PlayerDog;
        dog2 = GameManager.GetNextEnemy();

        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;

        dog1Anim = dog1.GetComponent<DogAnim>();
        dog2Anim = dog2.GetComponent<DogAnim>();

        dog1Anim.blood.SetActive(false);
        dog2Anim.blood.SetActive(false);

        dog1.biteIsLocked = dog2.biteIsLocked = false;

        //feedback.FeedSingle(StartFight(dog1, dog2));
        StartCoroutine(OpeningRoutine());
    }

	IEnumerator OpeningRoutine() {
		feedback.Feed (StartFight(dog1, dog2));
		yield return new WaitForSeconds (waitSeconds+1f);
		feedback.Feed (AggressionRound());
		yield return new WaitForSeconds (waitSeconds+1f);
		feedback.StopFeed ();
	}
    
    private string StartFight(Dog dog1, Dog dog2)
    {
        fightRunning = true;
        //should we reset the current strength to strength
        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;

        dog1HealthBar.fillAmount = 1;
        dog2HealthBar.fillAmount = 1;

        dog1.healthBar = dog1HealthBar;
        dog2.healthBar = dog2HealthBar;

        

        Dog1Image.sprite = dog1.sprite;
        Dog2Image.sprite = dog2.sprite;
        Dog1Text.text = dog1.dogName;
        Dog2Text.text = dog2.dogName;

        this.dog1 = dog1;
        this.dog2 = dog2;

        SoundManager.SetBackgroundSound(SoundManager.BackgroundSound.Fight0);
        
        return "The fight between the " + dog1.race + ", '" + dog1 + "', and the "
            + dog2.race + ", '" + dog2 + "' has started.";
    }

    public string AggressionRound()
    {
        //should there be a random roll??

        sound.PlayBark();

        Dog aggressor;
        Dog victim;

        if (dog1.aggression > dog2.aggression)
        {
            aggressor = dog1;
            victim = dog2;
            dog1Anim.Play(DogAnim.animType.Attack);
        }
        else
        {
            aggressor = dog2;
            victim = dog1;
            dog2Anim.Play(DogAnim.animType.Attack);
        }

        var value = aggressor.aggression - victim.courage;

		if (value <= 0) 
            return aggressor + " barks vicously, frightening the closest spectators, " +
                   "but " + victim + " seems unaffected.";
        

        victim.currentStrength -= value;

        return aggressor + " barks vicously. " + victim + " looks at its owner, like a scared puppy. " +
               "How will it be able to deal with the viciousness of " + aggressor + "?";

    }

    private IEnumerator RoundRoutine()
    {
        Dog firstDog, seconDog;

        DogAnim animation, secondAnim;

        //  ----------------- SPEED ROUND --------------------

        if (dog1.GetFightSpeed() * Random.value > dog2.GetFightSpeed() * Random.value)
        {
            firstDog = dog1;
            seconDog = dog2;
            animation = dog1Anim;
            secondAnim = dog2Anim;
        }
        else
        {
            seconDog = dog1;
            firstDog = dog2;
            animation = dog2Anim;
            secondAnim = dog1Anim;
        }

        var str = seconDog.currentStrength;

        yield return new WaitForSeconds(waitSeconds);

        if(!firstDog.biteIsLocked)
            animation.Play(DogAnim.animType.Hit);

        var wait = sound.PlayBite();

        feedback.Feed(Bite(firstDog, seconDog));

        yield return new WaitForSeconds(wait);

        //if it got injured
        if (seconDog.currentStrength < str)
        {
            secondAnim.Play(DogAnim.animType.Hit);

            yield return new WaitForSeconds(sound.PlayWhine());
        }
        if(!seconDog.alive)
            {

                fightRunning = false;
			    yield return new WaitForSeconds (waitSeconds);
			    feedback.StopFeed ();
                yield break;
            }
        yield return new WaitForSeconds(waitSeconds);

        wait = sound.PlayBite();

        str = firstDog.currentStrength;

        if(!seconDog.biteIsLocked)
            secondAnim.Play(DogAnim.animType.Hit);


        feedback.Feed (Bite (seconDog, firstDog));

        yield return new WaitForSeconds(wait);

        //if it got injured
        if (firstDog.currentStrength < str)
        {
            animation.Play(DogAnim.animType.Hit);

            yield return new WaitForSeconds(sound.PlayWhine());
        }

        if (!firstDog.alive)
        {
            fightRunning = false;
			yield return new WaitForSeconds (waitSeconds);
			feedback.StopFeed ();
            yield break;
        }

        if (seconDog.biteIsLocked && firstDog.biteIsLocked)
        {
            seconDog.biteIsLocked = false;
            firstDog.biteIsLocked = false;
            yield return new WaitForSeconds(waitSeconds);

            feedback.Feed ("The dogs jaws are locked onto eachother.\nThe Fight pauses for a few minutes, while the organizors seperate the locked jaws with dirty steel bars.");
        }
		yield return new WaitForSeconds (waitSeconds);
		feedback.StopFeed ();
    }


    public void Round(FightAction chosenAction)
    {

        if(!fightRunning)
            EndFight();
        
        // ACTION RESOLUTION
        ResolvePlayerAction(chosenAction);

        StartCoroutine(RoundRoutine());
    }

    private void ResolvePlayerAction(FightAction action)
	{
        switch (action)
        {
		case FightAction.ThroatBite:
			//Debug.Log ("\"Go for the Throat, '" + dog1 + "'!\"");
			feedbackStr = "\"Go for the Throat, '" + dog1 + "'!\"";
                break;
            case FightAction.LockBite:
                //Debug.Log("\"Lock your jaws around its neck, '" + dog1 + "'!\"");
				feedbackStr = "\"Lock your jaws around its neck, '" + dog1 + "'!\"";
                break;
		case FightAction.Scratch:
				//Debug.Log ("\"Scratch it, '" + dog1 + "'!\"");
				feedbackStr = "\"Scratch it, '" + dog1 + "'!\"";
                break;
		case FightAction.Tackle:
				//Debug.Log ("\"Tackle it, '" + dog1 + "'!\"");
				feedbackStr = "\"Tackle it, '" + dog1 + "'!\"";
                break;
		case FightAction.Run:
				//Debug.Log ("You try to run from the fight, but one of the gangsters grabs you by the neck, and forces you to stay and watch.");
				feedbackStr = "You try to run from the fight, but one of the gangsters grabs you by the neck, and forces you to stay and watch.";
                break;
		case FightAction.UseItem:
				//Debug.Log ("You do not have any Items to use.");
				feedbackStr = "You do not have any Items to use.";
                break;
		case FightAction.SwitchDog:
				//Debug.Log ("You can not change dogs during this fight.");
				feedbackStr = "You can not change dogs during this fight.";
                break;
            default:
                break;
        }
		feedback.Feed(feedbackStr);
    }

    private static string Bite(Dog attacker, Dog victimDog)
    {

        if (attacker.biteIsLocked)
        {
            victimDog.currentStrength -= biteLockStrengthDecrease;
            return attacker + " still has its jaws locked around the skin of " + victimDog + ".";
        }
        
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.AppendLine(attacker + " snaps after "+ victimDog+ ".");

        var roll = Random.value;

        roll = victimDog.biteIsLocked ? roll * 0.75f : roll;


        if (roll < 0.1)
        {
            stringBuilder.AppendLine(attacker + " misses " + victimDog + ".");

        }
        else if (attacker.GetFightBite() *roll > victimDog.currentStrength)
        {
            victimDog.currentStrength = 0;
            stringBuilder.AppendLine(attacker + " bites "+ victimDog +" in the neck. Ripping through its windpipe..");

            //should probably make a lot of different death texts
            stringBuilder.AppendLine(victimDog + " is no more.");

            victimDog.alive = false;
        }
        else
        {
            //USE THIS IF FIGHTS SHOULD BE LESS RANDOM
            if (attacker.GetFightBite() > victimDog.currentStrength)
                victimDog.currentStrength -= (attacker.GetFightBite() ) - victimDog.currentStrength;

            stringBuilder.AppendLine(attacker + " bites " + victimDog + ".");
            
            if (Random.value > 0.7)
            {
                attacker.biteIsLocked = true;
                stringBuilder.AppendLine("Its jaws locking onto the skin of " + victimDog + "'s neck.");
            }
        }

        return stringBuilder.ToString();
    }

    private void EndFight()
    {
        dog1Anim.CleanUp();
        dog2Anim.CleanUp();

        foreach (var go in ToggleAfterMatch)
        {
            go.SetActive(!go.activeSelf);
        }
    }
    
}
