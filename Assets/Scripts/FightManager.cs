using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    [Header("Feedback stuff")]
    public GameObject[] panels;
    public TextAnim display;
    
    [Header("Dog stuff")]
    private bool fightRunning;

    public Dog dog1, dog2;

    public bool FirstFight = true;

    public DogAnim dog1Anim, dog2Anim;

    /// Removed the healtbars
    //public Image dog1HealthBar, dog2HealthBar;

    public Text Dog1Text, Dog2Text;

    public Image Dog1Image, Dog2Image;

    private static readonly float waitSeconds = 2.8f;

    private static readonly float biteLockStrengthDecrease = 0.20f;

    public SoundManager sound;

	private string feedbackStr = "";

    private string nextMessage = "";
    
    public GameObject[] ToggleAfterMatch;
    public GameObject[] ToggleAfterDefeat;
    public GameObject[] ToggleAfterLastDogDead;

    private bool roundRunning;

    public float AutoPickAfterSeconds;

    public bool lastMatch = false;
    private bool doneShouting;

    public enum FightAction
    {
        ThroatBite,
        LockBite,
        Tackle,
        Scratch,
        UseItem,
        SwitchDog,
        Run,
        NoAction,



        Count
    }

    public void StartFight()
    {
        dog1 = GameManager.PlayerDog;
        dog2 = GameManager.GetNextEnemy();

        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;

        lastMatch = dog2.dogName == "El Bruto";

        dog1Anim.blood.SetActive(false);
        dog2Anim.blood.SetActive(false);

        dog1.biteIsLocked = dog2.biteIsLocked = false;

        GameManager.NextFight();

        //feedback.FeedSingle(StartFight(dog1, dog2));
        StartCoroutine(OpeningRoutine());
    }

	IEnumerator OpeningRoutine()
	{
        ShowFeedbackWindow();
        var feedStr = StartFight(dog1, dog2);

        display.Play(feedStr);
        yield return new WaitForSeconds(2f + feedStr.Length * 0.03f);
        
        doneShouting = false;
        StartCoroutine(PickAfterSeconds());
        yield return new WaitUntil(() => doneShouting);

        feedStr = AggressionRound();
        display.Play (feedStr);
        yield return new WaitForSeconds(2.5f );
	    if (feedStr.Contains("scared puppy"))
	    {
            sound.PlayWhine();
	    }
        yield return new WaitForSeconds(feedStr.Length * 0.03f);
        doneShouting = false;
        StartCoroutine(PickAfterSeconds());
        yield return new WaitUntil(() => doneShouting);

        StartCoroutine(RoundRoutine());
    }

    void ShowFeedbackWindow()
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(go.name.Contains("Feedback"));
        }
    }
    
    private string StartFight(Dog dog1, Dog dog2)
    {
        fightRunning = true;
        //should we reset the current strength to strength
        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;
        
        Dog1Image.sprite = dog1.sprite;
        Dog2Image.sprite = dog2.sprite;
        Dog1Text.text = dog1.dogName;
        Dog2Text.text = dog2.dogName;

        this.dog1 = dog1;
        this.dog2 = dog2;

        SoundManager.SetBackgroundSound(SoundManager.BackgroundSound.Fight0, true);
        
        return "The fight between the " + Dog.AsString(dog1.race) + ", '" + dog1 + "', and the "
            + Dog.AsString(dog2.race) + ", '" + dog2 + "' has started.";
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
               "\nHow will "+victim+" be able to deal with the viciousness of " + aggressor + "?";
    }

    private IEnumerator RoundRoutine()
    {
        roundRunning = true;

        if(feedbackStr.Length > 0)
            yield return new WaitForSeconds(2f + 0.03f * feedbackStr.Length);
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

        ShowFeedbackWindow();

        if(!firstDog.biteIsLocked)
            animation.Play(DogAnim.animType.Attack);
        else
        {
            //animation.Play(DogAnim.animType.Hit);
            animation.Play(DogAnim.animType.Locking);
            secondAnim.Play(DogAnim.animType.Locked);
        }

        var wait = sound.PlayBite();

        display.Play(Bite(firstDog, seconDog));

        yield return new WaitForSeconds(wait);
        

        //if it got injured
        if (seconDog.currentStrength < str)
        {
            secondAnim.Play(DogAnim.animType.Hit);

            yield return new WaitForSeconds(sound.PlayWhine());
        }

        if (!seconDog.alive)
            {
                fightRunning = false;
			    yield return new WaitForSeconds (waitSeconds);
                secondAnim.Play(DogAnim.animType.Death);
                yield return new WaitForSeconds(waitSeconds +2);

                Round(FightAction.NoAction);
                //HideFeedbackWindow();
                yield break;
            }

        yield return new WaitUntil(() => !display.isAnimPlaying);
        yield return new WaitForSeconds(1.5f);


        doneShouting = false;
        StartCoroutine(PickAfterSeconds());
        yield return new WaitUntil(() => doneShouting);
        
        wait = sound.PlayBite();

        str = firstDog.currentStrength;

        if(!seconDog.biteIsLocked)
            secondAnim.Play(DogAnim.animType.Attack);
        else
        {
            //secondAnim.Play(DogAnim.animType.Hit);
            secondAnim.Play(DogAnim.animType.Locking);
            animation.Play(DogAnim.animType.Locked);
        }


        display.Play (Bite (seconDog, firstDog));

        yield return new WaitForSeconds(wait);

        //if it got injured
        if (firstDog.currentStrength < str)
        {
            animation.Play(DogAnim.animType.Hit);

            //TODO: only if hurt enough, maybe make a slight injury sound
            yield return new WaitForSeconds(sound.PlayWhine());
        }

        if (!firstDog.alive)
        {
            fightRunning = false;
            yield return new WaitForSeconds(waitSeconds);
            animation.Play(DogAnim.animType.Death);
			yield return new WaitForSeconds (waitSeconds+2);
            Round(FightAction.NoAction);
            //HideFeedbackWindow();
            yield break;
        }
        yield return new WaitUntil(() => !display.isAnimPlaying);
        yield return new WaitForSeconds(1.5f);


        if (seconDog.biteIsLocked && firstDog.biteIsLocked)
        {
            seconDog.biteIsLocked = false;
            firstDog.biteIsLocked = false;

            animation.Play(DogAnim.animType.Locked);
            secondAnim.Play(DogAnim.animType.Locked);

            display.Play ("The dogs jaws are locked onto eachother.\nThe Fight pauses for a few minutes, while the organizors seperate the locked jaws with dirty steel bars.");

            yield return new WaitForSeconds(waitSeconds + 6);
        }

        doneShouting = false;
        StartCoroutine(PickAfterSeconds());
        yield return new WaitUntil(() => doneShouting);


        StartCoroutine(RoundRoutine());
    }
    

    public void ShoutAction(int shout)
    {
        if (shout == -1 ||!fightRunning) return;

        Round((FightAction)shout);
    }

    public void Round(FightAction chosenAction)
    {
        
        if (fightRunning)
        {
            // ACTION RESOLUTION
            ResolvePlayerAction(chosenAction);
        }
        else
        {
            if (!GameManager.PlayerDog.alive)

                Defeat();
            else
                EndFight();
        }
    }

    //Sets the next message to a corresponding player shout
    private void ResolvePlayerAction(FightAction action)
	{
        Debug.Log("fight aciton " + action);

        switch (action)
        {
		case FightAction.ThroatBite:
			//Debug.Log ("\"Go for the Throat, '" + dog1 + "'!\"");
			nextMessage = "\"Go for the Throat, '" + dog1 + "'!\"";
                break;
            case FightAction.LockBite:
                //Debug.Log("\"Lock your jaws around its neck, '" + dog1 + "'!\"");
                nextMessage = "\"Lock your jaws around its neck, '" + dog1 + "'!\"";
                break;
		case FightAction.Scratch:
                //Debug.Log ("\"Scratch it, '" + dog1 + "'!\"");
                nextMessage = "\"Scratch it, '" + dog1 + "'!\"";
                break;
		case FightAction.Tackle:
                //Debug.Log ("\"Tackle it, '" + dog1 + "'!\"");
                nextMessage = "\"Tackle it, '" + dog1 + "'!\"";
                break;
		case FightAction.Run:
                //Debug.Log ("You try to run from the fight, but one of the gangsters grabs you by the neck, and forces you to stay and watch.");
                nextMessage = "You try to run from the fight, but one of the gangsters grabs you by the neck, and forces you to stay and watch.";
                break;
		case FightAction.UseItem:
                break;
		case FightAction.SwitchDog:
                break;
            case FightAction.NoAction:
                return;
            default:
                break;
        }
	}

    private string Bite(Dog attacker, Dog victimDog)
    {
        if (attacker.biteIsLocked)
        {
            victimDog.currentStrength -= biteLockStrengthDecrease * attacker.currentStrength;
            return attacker + " still has its jaws locked around the skin of " + victimDog + ".";
        }
        
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.AppendLine(attacker + " snaps after "+ victimDog+ ".");

        var roll = Random.value;

        roll = victimDog.biteIsLocked ? roll * 0.60f : roll;


        if (roll < 0.2)
        {
            stringBuilder.AppendLine(attacker + " misses " + victimDog + ".");
        }
        else if (attacker.GetFightBite() *roll > victimDog.currentStrength || 
            //if first fight and going poorly for player dog
            (FirstFight &&attacker ==GameManager.PlayerDog 
                && (attacker.currentStrength/attacker.strength)<0.2f && (victimDog.currentStrength/victimDog.strength )< 0.5f))
        {
            victimDog.currentStrength = 0;
            stringBuilder.AppendLine(attacker + " bites " + victimDog +
                                        " in the neck. Ripping through its windpipe..");

            //should probably make a lot of different death texts
            stringBuilder.AppendLine(victimDog + " is no more.");

            victimDog.alive = false;
        }
        else
        {
            //USE THIS IF FIGHTS SHOULD BE LESS RANDOM
            victimDog.currentStrength -= (attacker.GetFightBite()*roll /3);

            stringBuilder.AppendLine(attacker + " bites " + victimDog + ".");
            
            if (Random.value > (victimDog.biteIsLocked ? 0.2 : 0.7) )
            {
                attacker.biteIsLocked = true;
                stringBuilder.AppendLine("Its jaws locking onto the skin of " + victimDog + "'s neck.");
            }
        }

        return stringBuilder.ToString();
    }

    private IEnumerator PickAfterSeconds()
    {
        if(nextMessage != "")
            //Play player shout!
        {
            display.Play(nextMessage);
            //Maybe wait for a little while
            
            yield return new WaitForSeconds(2f);


            nextMessage = "";
        }

        doneShouting = true;
        
    }

    private void EndFight()
    {
        GameManager.TickUpEnemy();

        dog1.kills++;

        FirstFight = false;


        GameManager.Money += dog2.BeatThisDogPrize;

        dog1Anim.CleanUp();
        dog2Anim.CleanUp();

        foreach (var go in ToggleAfterMatch)
        {
            go.SetActive(!go.activeSelf);
        }
    }

    private void Defeat()
    {
        GameManager.TickUpEnemy();

        dog2.kills++;

        GameManager.PutEnemyDogBackOnSchedule();

        dog1Anim.CleanUp();
        dog2Anim.CleanUp();

        GameManager.Money += dog2.LosePrize;

        if(GameManager.PlayerDogs.Any(d=>d.alive))
        {
            GameManager.PlayerDog = GameManager.PlayerDogs.First(d => d.alive);

            GameManager.PlayerDogs.RemoveAll(d => !d.alive);

            foreach (var go in ToggleAfterDefeat)
            {
                go.SetActive(!go.activeSelf);
            }

        }
        else
            foreach (var go in ToggleAfterLastDogDead)
            {
                Debug.Log("GAME OVER!!");

                go.SetActive(!go.activeSelf);
            }
    }

}
