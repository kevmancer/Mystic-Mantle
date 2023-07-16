using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject settingsMenu;
    private GameObject backButton;
    private bool showingMainMenu = true, isOverlay;
    private SettingsManager settingsManager;

    // Start is called before the first frame update
    void Start()
    {
        showingMainMenu = true;
        mainMenu = GameObject.Find("Main Menu").gameObject;
        settingsMenu = GameObject.Find("Settings Menu").gameObject;
        backButton = GameObject.Find("Back").gameObject;
        settingsManager = GameObject.Find("Settings Menu").GetComponent<SettingsManager>();
        GameManager.instance.menu = gameObject;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            isOverlay = false;
            GameManager.instance.ShowMenu();
        }
        else
        {
            isOverlay = true;
            GameManager.instance.CloseMenu();
        }
        backButton.SetActive(false);
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBack();
        }
    }

    public void OnContinue()
    {
        if (isOverlay)
        {
            GameManager.instance.CloseMenu();
        }
        else
        {
            SceneManager.LoadScene(GameManager.instance.gameData.currentLevel);
        }
    }

    public void OnNewGame()
    {
        GameManager.instance.ResetGameData();
        SceneManager.LoadScene(GameManager.instance.gameData.currentLevel);
    }

    public void OnSettings()
    {
        ShowSettings();
    }

    public void OnExit()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void OnBack()
    {
        if (showingMainMenu)
        {
            if (isOverlay)
            {
                GameManager.instance.CloseMenu();
            }
        }
        else
        {
            GameManager.instance.SaveSettings(settingsManager.settingsData);
            ShowMainMenu();
        }
    }

    void ShowSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        backButton.SetActive(true);
        showingMainMenu = false;
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        backButton.SetActive(false);
        showingMainMenu = true;
    }

}
