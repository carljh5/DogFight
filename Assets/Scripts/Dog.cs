using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dog : MonoBehaviour
{
    private string dogName;
    //females should have defined when they are in heat. 
    public bool male;
    //could be a mix of races, but probably a good idea with one dominating for art purposes
    public Race race;

    public GameObject prefab;
    
    [Header("Fighting stats (10 = normal)")]
    //variables should probably have a max and a min.
    //could also be obscured for the player
    //Used to overwhelm the opponent
    public float aggression = 10;
    // influences how much stamina the dog has, also influences bite,precision and agility
    public float strength = 10;
    //anti-aggression
    public float courage = 10;
    //which dog bites first
    public float speed = 10;
    //how hard the bite. Also used to grip the opponent dog
    public float bite = 10;
    [HideInInspector]
    public float currentStrength;
    
    public bool alive = true;
    public bool biteIsLocked = false;


    [Header("Unused variables")]
    //<= 0 is sterile
    public float virility;
    //could be obscured a bit to the player
    public float age;
    public float obidience;
    //how good the dog is at learning/understanding commans
    public float intelligence;

    //should probably be a seperate class, with associated standard values
    public enum Race
    {
        Mutt,
        SibereanHusky,
        GoldenRetriever,
        Boxer,
        BorderCollie,
        EnglishBullTerrier,
        Rottweiler,
        AmericanPitbullTerrier,
        Chihuahua
    }

    public void NameDog(string name)
    {
        dogName = name;
    }

    public float GetFightSpeed()
    {
        return (currentStrength / strength) * speed;
    }

    public float GetFightBite()
    {

        return currentStrength/strength > 0.5 ?(currentStrength / strength) * bite : bite * 0.5f;
    }

    public override string ToString()
    {
        return dogName;
    }

    //public void Train
}
