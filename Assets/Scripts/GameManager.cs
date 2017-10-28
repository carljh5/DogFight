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
        UnleashDog,
        GoToNextFight
    }

    public static string PlayerName;

    public static Dog PlayerDog;

    public static bool LeashDog;
    
    public Dog[] EnemyDogs;

    public Dog[] Dogs;

    private static GameManager instance;

    private int nextEnemyDogIdx;

    private FightManager fightManager;

    void Awake()
    {
        if (instance == null)
            instance = this;

        //TODO: Delete this later:
        PlayerDog = instance.Dogs[0];

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
            case GameEvent.LeashDog:
            case GameEvent.UnleashDog:
                //TODO: make sure this is called
                LeashDog = gameEvent == GameEvent.LeashDog;
                break;
            case GameEvent.GoToNextFight:
                instance.GoToNextFight();
                break;
            case GameEvent.IllegalEvent:
            default:
                Debug.LogWarning("illegal or unhandled GameEvent triggered: " + gameEvent);
                break;
        }
    }

    private void GoToNextFight()
    {
        if (fightManager == null)
            fightManager = GetComponent<FightManager>();

        if (EnemyDogs.Length <= nextEnemyDogIdx)
            nextEnemyDogIdx = EnemyDogs.Length - 1;

        fightManager.dog1 = PlayerDog;
        fightManager.dog2 = EnemyDogs[nextEnemyDogIdx++];
    }

    public static void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            PlayerName = "Fulano";
        else
            PlayerName = name;

        Debug.Log("Player is called " + PlayerName);
    }

    public static Dog GetNextEnemy()
    {
        return instance.EnemyDogs[instance.nextEnemyDogIdx];
    }
}
