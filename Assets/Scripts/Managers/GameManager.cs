using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameData gameData { get; private set; }
    public SettingsData settingsData { get; private set; }
    public GameObject menu;
    public bool gamePaused=false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        settingsData = SaveManager.LoadSettingsData();
        gameData = SaveManager.LoadGameData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                ShowMenu();
            }
        }
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void LoadLastSave()
    {
        SceneManager.LoadScene(gameData.currentLevel);
    }

    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene+1);
        SaveGameData(currentScene + 1);
    }

    public void SaveSettings(SettingsData settings)
    {
        settingsData.SetSettings(settings);
        SaveManager.SaveSettingsData(settingsData);
    }

    public void ResetSettings()
    {
        settingsData.ResetSettings();
        SaveManager.SaveSettingsData(settingsData);
    }

    public void SaveGameData(int sceneIndex)
    {
        GameData currentGameData = new GameData();
        currentGameData.currentLevel = sceneIndex;
        Entity entity = GameObject.Find("Player").GetComponent<Entity>();
        currentGameData.currentHealth = entity.currentHealth;
        gameData.SetGameData(currentGameData);
        SaveManager.SaveGameData(gameData);
    }

    public void ResetGameData()
    {
        gameData.ResetGameData();
        SaveManager.SaveGameData(gameData);
    }
}
