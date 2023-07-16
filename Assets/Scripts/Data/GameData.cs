using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel = 1;
    public float currentHealth = Constants.maxHealth1;

    public void SetGameData(GameData gameData)
    {
        currentLevel = gameData.currentLevel;
        currentHealth = gameData.currentHealth;
    }

    public void ResetGameData()
    {
        currentLevel = 1;
        currentHealth = Constants.maxHealth1;
    }

}
