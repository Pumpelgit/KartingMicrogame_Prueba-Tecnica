using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{

    public static Action OnSaveData;
    public static Action<float> OnSaveBestTime;
    public static Action<int> OnSaveTotalCoins;

    public static Action<PlayerData> OnLoadData;
    public string fileName = "PlayerData.json";

    [SerializeField]
    PlayerData runtimePlayerData;

    [SerializeField]
    bool saveOnDestroy = false;

    string filePath { 
        get { 
            return Application.persistentDataPath + "/" + fileName; 
        } 
    }

    private void Start()
    {
        LoadPlayerData();
    }
    void OnEnable()
    {
        OnSaveData += SavePlayerData;
        OnSaveBestTime += SaveBestTimeData;
        OnSaveTotalCoins += SaveCoinData;
    }

    void OnDisable()
    {
        OnSaveData -= SavePlayerData;
        OnSaveBestTime -= SaveBestTimeData;
        OnSaveTotalCoins -= SaveCoinData;
    }

    private void OnDestroy()
    {
        if(saveOnDestroy)
        {
            SavePlayerData();
        }
    }

    void SavePlayerData()
    {
        string savedJson = "";
        savedJson = JsonUtility.ToJson(runtimePlayerData);
        File.WriteAllText(filePath, savedJson);
    }

    void SaveCoinData(int coinAmount)
    {
        runtimePlayerData.totalCoins += coinAmount;
    }

    void SaveBestTimeData(float bestTime)
    {
        runtimePlayerData.bestTime = bestTime;
    }

    void LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string textRead = File.ReadAllText(filePath);
            runtimePlayerData = JsonUtility.FromJson<PlayerData>(textRead);
            if(OnLoadData!=null)
                OnLoadData.Invoke(runtimePlayerData);
        }
        else
        {
            runtimePlayerData = new PlayerData();
            Debug.Log("No Player Data Found, Creating Data For First Time");
        }
    }

}
