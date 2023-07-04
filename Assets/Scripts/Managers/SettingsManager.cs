using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public SettingsData settingsData;
    private Slider fxAudio,musicAudio,masterAudio;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        fxAudio = GameObject.Find("Fx Audio Slider").GetComponent<Slider>();
        musicAudio = GameObject.Find("Music Audio Slider").GetComponent<Slider>();
        masterAudio = GameObject.Find("Master Audio Slider").GetComponent<Slider>();
        settingsData = GameManager.instance.settingsData;
        SetUIFromData();
    }

    public void SetMasterVolume(float level)
    {
        float value = Mathf.Log10(level) * 20f;
        audioMixer.SetFloat("masterVolume", value);
        settingsData.audioMaster = level;
    }

    public void SetAudioFxVolume(float level)
    {
        float value = Mathf.Log10(level) * 20f;
        audioMixer.SetFloat("audioFxVolume", value);
        settingsData.audioFx = level;
    }

    public void SetMusicVolume(float level)
    {
        float value = Mathf.Log10(level) * 20f;
        audioMixer.SetFloat("musicVolume", value);
        settingsData.audioMusic = level;
    }

    private void SetUIFromData()
    {
        fxAudio.value = settingsData.audioFx;
        audioMixer.SetFloat("audioFxVolume", Mathf.Log10(settingsData.audioFx) * 20f);
        musicAudio.value = settingsData.audioMusic;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(settingsData.audioMusic) * 20f);
        masterAudio.value = settingsData.audioMaster;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(settingsData.audioMaster) * 20f);
    }

    public void OnResetSettings()
    {
        settingsData.ResetSettings();
        SetUIFromData();
    }
}
