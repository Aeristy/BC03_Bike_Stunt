using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public PlayerModel()
    {
        ResetData();
    }

    private void ResetData()
    {
        chooseLang = false;
        chooseAge = false;
        age = 0;
        currentLang = "";
        currentInstructor = 1;

        //level + car data
        currentBike = 1;
        currentLevel = 1;
        currentMode = 1;
        currentColor = 0;
        currentWheel = 1;
        currentTire = 1;
        unlockLevel = 1;
        currentPlayerLevel = 1;
        currentExp = 0;
        currentSubdayAds = 0;
        

        // ----- Setting System -----
        sound = 1f;
        music = 1f;
        quality = 2;
        vibrate = true;

        // ----- Setting Steering -----
        useMPH = false;
        doubleCoin = false;

        currentGold = 0;




    }


    [Header("SETTING")]
    public float sound;
    public float music;
    public int quality;
    public bool vibrate;
    public string currentLang;
    public int currentInstructor;

    //ingame setting
    public float sensitivity;
    public bool useMPH;

    [Header("CURRENT DATA")]
    public int currentBike;
    public int currentLevel;
    public int currentMode;
    public int currentColor;
    public int currentWheel;
    public int currentTire;
    public int currentSpoiler;
    public int unlockLevel;
    public int currentPlayerLevel;
    public int currentExp;
    public int currentSubdayAds;
    public DateTime subdayDate;

    [Header("GAME DATA")]
    public bool chooseLang;
    public bool chooseAge;
    public bool doubleCoin;
    public int age;

    [Header("CURRENCY")]
    public int currentGold;
    public int currentGem;



   
}




[Serializable]
public class PlayerLevel
{
    public PlayerLevel()
    {
        levelId = 1;
        SetDefaultData();
    }

    public PlayerLevel(int _levelId)
    {
        levelId = _levelId;
        SetDefaultData();
    }

    private void SetDefaultData()
    {
        levelTime = 0;
        collectedCoin = 0;
        currentTime = 0;
        bestTime = 0;
        currentStar = 0;
        firstTime = true;
        unlock = false;
        current = false;
        score = 0;
        Mode = LEVEL_MODE.STUNT;

    }

    public int levelId;
    public float levelTime;
    public int collectedCoin;
    public long currentTime;
    public long bestTime;
    public int currentStar;
    public bool firstTime;
    public bool unlock;
    public bool current;
    public int score;
    public LEVEL_MODE Mode = LEVEL_MODE.STUNT;

}




[Serializable]
public class PlayerCustom
{
    public PlayerCustom(int _carId, int _itemId)
    {
        carId = _carId;
        itemId = _itemId;
    }

    public int carId;
    public int itemId;
}
[Serializable]


public enum PRICE_TYPE
{
    FREE, GOLD, GEM, ADS, LEVEL, DAY
}


public enum LEVEL_MODE
{
    STUNT = 0,
}