using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FightPresentation : MonoBehaviour
{
    public Image EnemyImage, PlayerImage;
    public Text EnemyText, PlayerText;
    public Text WinMoneyText, LoseMoneyText;
    private static FightPresentation instance;
    public Dropdown DogSelectDropDown;


    void OnEnable()
    {
        if (instance == null)
            instance = this;

        GameManager.SetPanelActive(false);
        

        var drops = GameManager.PlayerDogs.Select(d => new Dropdown.OptionData(d.dogName,d.sprite)).ToList();

        DogSelectDropDown.options.Clear();

        DogSelectDropDown.AddOptions(drops);

        setDogs();
    }

    void OnDisable()
    {
        GameManager.SetPanelActive(true);
    }

    public void PickDog(int i)
    {
        GameManager.PlayerDog = GameManager.PlayerDogs[i];
        setDogs();
    }

    private void setDogs()
    {
        var e = GameManager.GetNextEnemy();
        var p = GameManager.PlayerDog;

        WinMoneyText.text = "$ " +  e.BeatThisDogPrize;
        LoseMoneyText.text = "$ " + e.LosePrize;

        EnemyImage.sprite = e.sprite;

        PlayerImage.sprite = p.sprite;

        EnemyText.text = e.dogName + ", the " + Dog.AsString(e.race);

        PlayerText.text = p.dogName + ", the " + Dog.AsString(p.race);
    }

    public static void Reset()
    {
        instance.setDogs();
    }
}
