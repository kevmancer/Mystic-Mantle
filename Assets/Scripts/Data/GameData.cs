using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel = 0;
    public float currentHealth = Constants.maxHealth1;

    public void SetGameData(GameData gameData)
    {
        currentLevel = gameData.currentLevel;
        currentHealth = gameData.currentHealth;
    }

    public void ResetGameData()
    {
        currentLevel = 0;
        currentHealth = Constants.maxHealth1;
    }

}
