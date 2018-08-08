using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        GoToNextFight,
        NoAction,
        RestartGame
    }

    public static string PlayerName;

    public static Dog PlayerDog;

    public GameObject[] FightLocations;
	public GameObject[] WeekOptions;
    public int lastLocation;

    public static bool LeashDog;
    
    public Dog[] EnemyDogs;

    public static List<Dog> PlayerDogs;

    public Dog[] Dogs;

    public GameObject TextPanelGameObject;

    //[Serializable]
    //public struct KeyVal
    //{
    //    public string Key;
    //    public string Value;
    //}

    //public KeyVal[] EscapeKeyValuePairs;

    [Header("EscapeWords")]
    public string DogNameEscapeWord = "@dogName";
    public string WinMoneyEscapeWord = "@win";
    public string LoseMoneyEscapeWord = "@lose";

    private Dictionary<string,string> EscapeWords = new Dictionary<string, string>();

    private static GameManager instance;

    private int nextEnemyDogIdx;

    public int Day = 0;

    public static int Money = 0;

    public int CostPrDog = 10;
    
    public Text DayText, MoneyText, DailyCostText;



    private FightManager fightManager;

    void Start()
    {
        Screen.SetResolution(450, 800, false);

        PlayerDogs = new List<Dog>();
        //PlayerDogs = Dogs.ToList();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        //TODO: Delete this later:
        PlayerDog = instance.Dogs[1];

        
        //foreach (var kv in EscapeKeyValuePairs)
        //{
        //    EscapeWords[kv.Key] = kv.Value;
        //}

        Debug.Log("Game manager initialized.");
    }

    private void FixedUpdate()
    {
        MoneyText.text = Money.ToString();
        DayText.text = Day.ToString();
        DailyCostText.text = DailyCost().ToString();
    }


    public static void SetPanelActive(bool active)
    {
        instance.TextPanelGameObject.SetActive(active);
    }

    public static void NextDay()
    {
		if (instance.Day == 0)
			foreach (GameObject GO in instance.WeekOptions) {
				GO.SetActive (true);
			}

        instance.Day++;

        Money -= DailyCost();

		instance.SetActiveFights ();

    }

    public void SetActiveFights()
    {
        foreach(var loc in FightLocations)
        {
            loc.SetActive(false);
        }

        if (Day % 2 == 0)
        {
            FightLocations[++lastLocation % FightLocations.Length].SetActive(true);
        }
    }

    public static int DailyCost()
    {
        return PlayerDogs.Count * instance.CostPrDog;
    }

    public static void OptionSelected(GameEvent gameEvent)
    {
        Debug.Log("EVENT: "+gameEvent);

        switch (gameEvent)
        {
            case GameEvent.Dog1Selected:
                PlayerDog = instance.Dogs[0];
                PlayerDogs.Add(PlayerDog);
                break;

            case GameEvent.Dog2Selected:
                PlayerDog = instance.Dogs[1];
                PlayerDogs.Add(PlayerDog);
                break;
            case GameEvent.Dog3Selected:
                PlayerDog = instance.Dogs[2];
                PlayerDogs.Add(PlayerDog);
                break;
            case GameEvent.LeashDog:
            case GameEvent.UnleashDog:
                //TODO: make sure this is called
                LeashDog = gameEvent == GameEvent.LeashDog;
                break;
            case GameEvent.GoToNextFight:
                instance.GoToNextFight();
                break;
            case GameEvent.NoAction:
                break;
            case GameEvent.RestartGame:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GameEvent.IllegalEvent:
            default:
                Debug.LogWarning("illegal or unhandled GameEvent triggered: " + gameEvent);
                break;
        }
    }

    public static void NextFight()
    {
        instance.GoToNextFight();
    }


    public void PickDog(int i)
    {
        PlayerDog = PlayerDogs[i];
    }

        private void GoToNextFight()
    {
        if (fightManager == null)
            fightManager = GetComponent<FightManager>();

        if (EnemyDogs.Length <= nextEnemyDogIdx)
            nextEnemyDogIdx = EnemyDogs.Length - 1;

        fightManager.dog1 = PlayerDog;
        fightManager.dog2 = EnemyDogs[nextEnemyDogIdx++];

        EscapeWords[WinMoneyEscapeWord] = "$ " + fightManager.dog2.BeatThisDogPrize;
        EscapeWords[LoseMoneyEscapeWord] = "$ " + fightManager.dog2.LosePrize;

        FightPresentation.Reset();
    }

    /// <summary>
    /// Depreceated
    /// </summary>
    /// <param name="name"></param>
    public static void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            PlayerName = "Fulano";
        else
            PlayerName = name;

        //instance.EscapeWords[instance.PlayerNameEscapeWord] = PlayerName;

        Debug.Log("Player is called " + PlayerName);
    }


    public static void SetDogName(string name)
    {
        if (string.IsNullOrEmpty(name))
            PlayerDogs.Last().dogName = "Chicharito";
        else
            PlayerDogs.Last().dogName = name;

        instance.EscapeWords[instance.DogNameEscapeWord] = PlayerDog.dogName;

        Debug.Log("Dog is called " + PlayerName);
    }
    
    public static Dog GetNextEnemy()
    {
        return instance.EnemyDogs[instance.nextEnemyDogIdx];
    }

    public static string Clean( string s)
    {
        StringBuilder returnstring = new StringBuilder();

        foreach (var word in s.Split(' '))
        {
            if (instance.EscapeWords.ContainsKey(word))
            {
                returnstring.Append(instance.EscapeWords[word] + " ");
            }
            else
            {
                returnstring.Append(word + " ");
            }
        }

        return returnstring.ToString();
    }
}
