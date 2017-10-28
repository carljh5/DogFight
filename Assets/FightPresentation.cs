using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPresentation : MonoBehaviour
{
    public Image EnemyImage, PlayerImage;
    public Text EnemyText, PlayerText;

    void OnEnable()
    {
        var e = GameManager.GetNextEnemy();
        var p = GameManager.PlayerDog;


        EnemyImage.sprite = e.sprite;

        PlayerImage.sprite = p.sprite;

        EnemyText.text = e.dogName + ", the " + e.race.ToString();

        PlayerText.text = p.dogName + ", the " + p.race.ToString();
    }
}
