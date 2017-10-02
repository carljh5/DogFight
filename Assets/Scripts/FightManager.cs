using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    private static FightManager instance;

    private bool fightRunning;

    private Dog dog1, dog2;

    private bool firstDogLocked, secondDogLocked;

    private static readonly float biteLockStrengthDecrease = 0.5f;

    public enum FightAction
    {
        ThroatBite,
        LockBite,
        Tackle,
        Scratch,
        UseItem,
        SwitchDog,
        Run
    }
    
    public static string StartFight(Dog dog1, Dog dog2)
    {
        if (!instance)
        {
            instance = new FightManager();

        }

        instance.fightRunning = true;
        //should we reset the current strength to strength
        dog1.currentStrength = dog1.strength;
        dog2.currentStrength = dog2.strength;

        instance.dog1 = dog1;
        instance.dog2 = dog2;
        
        return "The fight between the " + dog1.race + ", " + dog1.name + ", and the "
            + dog2.race + ", " + dog2.name + " has started.";
    }

    public static string AggressionRound()
    {
        //should there be a random roll??

        Dog aggressor;
        Dog victim;

        if (instance.dog1.aggression > instance.dog2.aggression)
        {
            aggressor = instance.dog1;
            victim = instance.dog2;
        }
        else
        {
            aggressor = instance.dog2;
            victim = instance.dog1;
        }

        var value = aggressor.aggression - victim.courage;

        if (value <= 0)
            return aggressor + " barks vicously, frightening the closest spectators, " +
                   "but " + victim + " seems unaffected.";

        victim.currentStrength -= value;

        return aggressor + " barks vicously. " + victim + " looks at its owner, like a scared puppy. " +
               "How will it be able to deal with the viciousness of " + aggressor + "?";

    }

    public static string Round(FightAction chosenAction)
    {
        if(!instance.fightRunning)
             throw new ExecutionEngineException("Calling round while fight is not running. Are the dogs still alive?");

        StringBuilder stringBuilder = new StringBuilder();

        // ACTION RESOLUTION
        stringBuilder.AppendLine("Player used: " +chosenAction.ToString());


        Dog firstDog, seconDog;

        if (instance.dog1.GetFightSpeed() * Random.value > instance.dog2.GetFightSpeed() * Random.value)
        {
            firstDog = instance.dog1;
            seconDog = instance.dog2;
        }
        else
        {
            seconDog = instance.dog1;
            firstDog = instance.dog2;
        }

        stringBuilder.AppendLine(Bite(firstDog, seconDog));

        if (!seconDog.alive)
        {
            instance.fightRunning = false;
            return stringBuilder.ToString();
        }    
        
        stringBuilder.AppendLine(Bite(seconDog, firstDog));

        if (!firstDog.alive)
        {
            instance.fightRunning = false;
            return stringBuilder.ToString();
        }

        if (seconDog.biteIsLocked && firstDog.biteIsLocked)
        {
            seconDog.biteIsLocked = false;
            firstDog.biteIsLocked = false;
            stringBuilder.AppendLine("The dogs jaws are locked onto eachother.");

            stringBuilder.AppendLine("The Fight pauses for a few minutes, while the organizors seperate the locked jaws with dirty steel bars.");
        }

        return stringBuilder.ToString();
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
