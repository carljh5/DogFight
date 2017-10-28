using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameEvent
    {
        IllegalEvent,
        Dog1Selected,
        Dog2Selected,
        Dog3Selected,
        LeashDog,
        UnleashDog
    }

    public static string PlayerName;

    public static Dog PlayerDog;

    public static bool LeashDog;
    
    public Dog[] EnemyDogs;

    public Dog[] Dogs;

    private static GameManager instance;

    private int nextEnemyDogIdx;

    void Awake()
    {
        if (instance == null)
            instance = this;

        Debug.Log("Game manager initialized.");
    }

    public static void OptionSelected(GameEvent gameEvent)
    {
        Debug.Log("EVENT: "+gameEvent);

        switch (gameEvent)
        {
            case GameEvent.Dog1Selected:
                PlayerDog = instance.Dogs[0];
                break;

            case GameEvent.Dog2Selected:
                PlayerDog = instance.Dogs[1];
                break;
            case GameEvent.Dog3Selected:
                PlayerDog = instance.Dogs[2];
                break;
        }
    }

    public static void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            PlayerName = "Fulano";
        else
            PlayerName = name;

        Debug.Log("Player is called " + PlayerName);
    }
}
