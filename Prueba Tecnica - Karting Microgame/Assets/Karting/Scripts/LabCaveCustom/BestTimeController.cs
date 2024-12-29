using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTimeController : MonoBehaviour
{
    [SerializeField]
    private MenuElementView bestTimeView;


    private void OnEnable()
    {
        SaveManager.OnLoadData += SetBestTime;
    }

    private void OnDisable()
    {
        SaveManager.OnLoadData -= SetBestTime;
    }

    private void SetBestTime(PlayerData playerData)
    {
        if(playerData != null && playerData.bestTime >0)
        {
            bestTimeView.SetText(getTimeString(playerData.bestTime));
        }
        else
        {
            bestTimeView.SetText("No Time Recorded");
        }
    }

    string getTimeString(float time)
    {
        int timeInt = (int)(time);
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        float fraction = (time * 100) % 100;
        if (fraction > 99) fraction = 99;
        return string.Format("{0}:{1:00}:{2:00}", minutes, seconds, fraction);
    }
}
