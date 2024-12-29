using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalCoinController : MonoBehaviour
{
    [SerializeField]
    private MenuElementView textDisplay;

    private void OnEnable()
    {
        SaveManager.OnLoadData += SetTotalCoin;
    }

    private void OnDisable()
    {
       SaveManager.OnLoadData -= SetTotalCoin;
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
}
