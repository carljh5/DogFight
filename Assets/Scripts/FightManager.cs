using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    private bool fightRunning;

    private Dog dog1, dog2;

    private bool firstDogLocked, secondDogLocked;

    private static readonly float waitSeconds = 2f;

    private static readonly float biteLockStrengthDecrease = 0.5f;

    public SoundManager sound;

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
    
    public string StartFight(Dog dog1, Dog dog2)
    {
        fightRunning = true;
        //should we reset the current strength to strength
        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;

        this.dog1 = dog1;
        this.dog2 = dog2;
        
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
        }
        else
        {
            aggressor = dog2;
            victim = dog1;
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

        if (dog1.GetFightSpeed() * Random.value > dog2.GetFightSpeed() * Random.value)
        {
            firstDog = dog1;
            seconDog = dog2;
        }
        else
        {
            seconDog = dog1;
            firstDog = dog2;
        }
        yield return new WaitForSeconds(waitSeconds);

        var wait = sound.PlayBite();

        Debug.Log(Bite(firstDog, seconDog));

        yield return new WaitForSeconds(wait);

        yield return new WaitForSeconds(sound.PlayWhine());
        
        if (!seconDog.alive)
        {

            fightRunning = false;
            yield break;
        }
        yield return new WaitForSeconds(waitSeconds);

        wait = sound.PlayBite();

        Debug.Log(Bite(seconDog, firstDog));

        yield return new WaitForSeconds(wait);

        yield return new WaitForSeconds(sound.PlayWhine());

        if (!firstDog.alive)
        {
            fightRunning = false;
            yield break;
        }

        if (seconDog.biteIsLocked && firstDog.biteIsLocked)
        {
            seconDog.biteIsLocked = false;
            firstDog.biteIsLocked = false;
            yield return new WaitForSeconds(waitSeconds);
            Debug.Log("The dogs jaws are locked onto eachother.");

            Debug.Log("The Fight pauses for a few minutes, while the organizors seperate the locked jaws with dirty steel bars.");
        }

    }


    public void Round(FightAction chosenAction)
    {

        if(!fightRunning)
             throw new ExecutionEngineException("Calling round while fight is not running. Are the dogs still alive?");
        
        // ACTION RESOLUTION
        ResolvePlayerAction(chosenAction);

        StartCoroutine(RoundRoutine());
    }

    private void ResolvePlayerAction(FightAction action)
    {
        switch (action)
        {
            case FightAction.ThroatBite:
                Debug.Log("\"Go for the Throat, '"+dog1+"'!\"");
                break;
            case FightAction.LockBite:
                Debug.Log("\"Lock your jaws around its neck, '" + dog1 + "'!\"");
                break;
            case FightAction.Scratch:
                Debug.Log("\"Scratch it, '" + dog1 + "'!\"");
                break;
            case FightAction.Tackle:
                Debug.Log("\"Tackle it, '" + dog1 + "'!\"");
                break;
            case FightAction.Run:
                Debug.Log("You try to run from the fight, but one of the gangsters grabs you by the neck, and forces you to stay and watch.");
                break;
            case FightAction.UseItem:
                Debug.Log("You do not have any Items to use.");
                break;
            default:
                break;
        }

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
            stringBuilder.AppendLine(attacker + " bites "+ victimDog +" in the neck. Ripping through its windpipe..");

            //should probably make a lot of different death texts
            stringBuilder.AppendLine(victimDog + " is no more.");

            victimDog.alive = false;
        }
        else
        {
            //USE THIS IF FIGHTS SHOULD BE LESS RANDOM
            //if (attacker.GetFightBite() > victimDog.currentStrength) victimDog.currentStrength -= attacker.GetFightBite() - victimDog.currentStrength;

            attacker.biteIsLocked = true;
            stringBuilder.AppendLine(attacker + " bites "+ victimDog +  ". Its jaws locking onto the skin of "+victimDog +  "'s neck.");
        }

        return stringBuilder.ToString();
    }

    
}
