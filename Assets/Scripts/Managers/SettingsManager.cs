using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public SettingsData settingsData;
    private Slider fxAudio,musicAudio,masterAudio;
    private Toggle fullscreenTog, vsyncTog;
    private TMP_Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    public List<Resolution> resolutions = new List<Resolution>();

    // Start is called before the first frame update
    void Start()
    {
        fxAudio = GameObject.Find("Fx Audio Slider").GetComponent<Slider>();
        musicAudio = GameObject.Find("Music Audio Slider").GetComponent<Slider>();
        masterAudio = GameObject.Find("Master Audio Slider").GetComponent<Slider>();
        fullscreenTog = GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>();
        vsyncTog = GameObject.Find("Vsync Toggle").GetComponent<Toggle>();
        resolutionDropdown = GameObject.Find("Resolution Dropdown").GetComponent<TMP_Dropdown>();
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

    public void ApplyGraphics()
    {
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        int resWidth = int.Parse(resolutionDropdown.options[resolutionDropdown.value].text.Split("x", 2, System.StringSplitOptions.RemoveEmptyEntries)[0]);
        int resHeight = int.Parse(resolutionDropdown.options[resolutionDropdown.value].text.Split("x", 2, System.StringSplitOptions.RemoveEmptyEntries)[1]);
        Screen.SetResolution(resWidth, resHeight, fullscreenTog.isOn);
    }

    private void SetUIFromData()
    {
        fxAudio.value = settingsData.audioFx;
        audioMixer.SetFloat("audioFxVolume", Mathf.Log10(settingsData.audioFx) * 20f);
        musicAudio.value = settingsData.audioMusic;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(settingsData.audioMusic) * 20f);
        masterAudio.value = settingsData.audioMaster;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(settingsData.audioMaster) * 20f);
        fullscreenTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        foreach(Resolution resolution in resolutions)
        {
            if (Screen.width == resolution.horizontal && Screen.height == resolution.vertical)
            {
                foundRes = true;
                resolutionDropdown.value = resolutionDropdown.options.FindIndex(option => option.text == Screen.width.ToString() + "x" + Screen.height.ToString());
            }
        }
        if (!foundRes)
        {
            Resolution newRes = new Resolution(Screen.width, Screen.height);
            resolutions.Add(newRes);
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(Screen.width.ToString() + "x" + Screen.height.ToString()));
            resolutionDropdown.value = resolutionDropdown.options.FindIndex(option => option.text == Screen.width.ToString() + "x" + Screen.height.ToString());
        }
    }



    public void OnResetSettings()
    {
        settingsData.ResetSettings();
        SetUIFromData();
    }
}
