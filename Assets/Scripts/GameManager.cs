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


    public static Dog PlayerDog;

    public static bool LeashDog;
    
    public string[] EnemyDogs;

    private int nextEnemyDogIdx;

    public static void OptionSelected(GameEvent gameEvent)
    {
        Debug.Log("EVENT: "+gameEvent);

        switch (gameEvent)
        {
            
        }
    }

}
