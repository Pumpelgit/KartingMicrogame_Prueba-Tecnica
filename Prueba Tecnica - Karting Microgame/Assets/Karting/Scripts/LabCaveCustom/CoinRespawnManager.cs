using KartGame.Track;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRespawnManager : MonoBehaviour
{

    List<CoinObject> coinList = new List<CoinObject>();
    bool lapsFinishDontSpawn = false;


    private void Start()
    {
        coinList.AddRange(GetComponentsInChildren<CoinObject>());
        TimeDisplay.OnUpdateLap += EnableCoins;
        TimeDisplay.OnLapsFinished += DisableCoins;
    }

    private void EnableCoins()
    {
        if(lapsFinishDontSpawn)
        {
            return;
        }
        for (int i = 0; i < coinList.Count; i++)
        {
            coinList[i].gameObject.SetActive(true);
        }
    }

    private void DisableCoins()
    {
        lapsFinishDontSpawn = true;
        for (int i = 0; i < coinList.Count; i++)
        {
            coinList[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        TimeDisplay.OnUpdateLap -= EnableCoins;
        TimeDisplay.OnLapsFinished -= DisableCoins;
    }
}
