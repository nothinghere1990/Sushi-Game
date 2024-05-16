using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyController : MonoBehaviour
{
    [HideInInspector] public TMP_Text playerMoneyText;
    static public int PlayerMoney = 500;
    public int targetMoney;


    public void Start()
    {
        playerMoneyText = transform.GetComponent<TMP_Text>();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                playerMoneyText.text = "$" + PlayerMoney;
                break;
            case 2:
                playerMoneyText.text = "$" + PlayerMoney + " / " + "$" + targetMoney;
                break;
        }
    }

    public void StockingSpendMoney(int itemPrice)
    {
        PlayerMoney -= itemPrice;
        playerMoneyText.text = "$" + PlayerMoney;
    }

    public void StockingEarnMoney(int itemPrice)
    {
        PlayerMoney += itemPrice;
        playerMoneyText.text = "$" + PlayerMoney;
    }

    public void MakingEarnMoney(int orderPrice)
    {
        PlayerMoney += orderPrice;
        playerMoneyText.text = "$" + PlayerMoney + " / " + "$" + targetMoney;
    }
}