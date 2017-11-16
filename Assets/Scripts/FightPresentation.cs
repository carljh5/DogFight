using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPresentation : MonoBehaviour
{
    public Image EnemyImage, PlayerImage;
    public Text EnemyText, PlayerText;
    private static FightPresentation instance;

    void OnEnable()
    {
        if (instance == null)
            instance = this;

        GameManager.SetPanelActive(false);

        setDogs();
    }

    void OnDisable()
    {
        GameManager.SetPanelActive(true);
    }

    private void setDogs()
    {
        var e = GameManager.GetNextEnemy();
        var p = GameManager.PlayerDog;


        EnemyImage.sprite = e.sprite;

        PlayerImage.sprite = p.sprite;

        EnemyText.text = e.dogName + ", the " + e.race.ToString();

        PlayerText.text = p.dogName + ", the " + p.race.ToString();
    }

    public static void Reset()
    {
        instance.setDogs();
    }
}
