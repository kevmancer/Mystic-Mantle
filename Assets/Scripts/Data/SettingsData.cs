using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float audioFx = 1f;
    public float audioMusic = 1f;
    public float audioMaster = 1f;

    public void SetSettings(SettingsData settings)
    {
        audioMusic = settings.audioMusic;
        audioFx = settings.audioFx;
        audioMaster = settings.audioMaster;
    }

    public void ResetSettings()
    {
        audioFx = 1f;
        audioMusic = 1f;
        audioMaster = 1f;
    }

}
