using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Text health;
    [SerializeField] Text coin;
    public static UIController instance;
    public int coins;

    void Awake()
    {
        instance = this;
        coins = 0;
    }

    public void UpdateLives(int life)
    {
        this.health.text = "x " + life;
    }

    public void incrementCoin(int coin)
    {
        this.coins += coin;
        this.coin.text = this.coins.ToString();
    }

}
