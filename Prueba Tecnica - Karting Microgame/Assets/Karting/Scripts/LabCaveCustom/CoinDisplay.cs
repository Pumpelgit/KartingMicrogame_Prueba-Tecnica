using System.Collections;
using System;
using UnityEngine;
using TMPro;
using KartGame.Track;

public class CoinDisplay : MonoBehaviour
{
    public static Action OnCoinPickup;

    int totalCoins = 0;

    [SerializeField]
    protected TextMeshProUGUI textDisplay;

    [SerializeField]
    bool isMenuDisplay = false;

    private void OnEnable()
    {
        OnCoinPickup += AddCoin;
        if (isMenuDisplay)
            SaveManager.OnLoadData += SetTotalCoin;
        else
            TimeDisplay.OnLapsFinished += SaveTotalCoins;
    }

    private void OnDisable()
    {
        OnCoinPickup -= AddCoin;
        if (isMenuDisplay)
            SaveManager.OnLoadData -= SetTotalCoin;
        else
            TimeDisplay.OnLapsFinished -= SaveTotalCoins;
    }

    void SaveTotalCoins()
    {
        SaveManager.OnSaveTotalCoins(totalCoins);
    }

    private void SetTotalCoin(PlayerData playerData)
    {
        if (playerData != null)
        {
            textDisplay.SetText(playerData.totalCoins.ToString());
        }
        else
        {
            textDisplay.SetText("0");
        }
    }

    void AddCoin()
    {
        totalCoins++;
        textDisplay.SetText(totalCoins.ToString());
    }
}
