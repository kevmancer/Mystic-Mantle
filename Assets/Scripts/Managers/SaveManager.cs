using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static void SaveGameData(GameData currentData)
    {
        GameData data = currentData;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/gamedatasavefile.json", json);
    }

    public static void SaveSettingsData(SettingsData settingsData)
    {
        SettingsData settings = settingsData;

        string json = JsonUtility.ToJson(settings);

        File.WriteAllText(Application.persistentDataPath + "/settingsdatasavefile.json", json);
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gamedatasavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            return data;
        }
        else
        {
            return new GameData();
        }
    }

    public static SettingsData LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/settingsdatasavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsData settings = JsonUtility.FromJson<SettingsData>(json);

            return settings;
        }
        else
        {
            return new SettingsData();
        }
    }
}
