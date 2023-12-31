using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject mainmenuPanel;
    public GameObject savePanel;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Text coinText;
    public TMP_Text crystalText;
    public Image[] healthArray;
    public GameObject gameOverGroup;
 
    public AudioMixer audioMixer;
    

    private static UIManager instance;
    private Resolution[] resolutions;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        resolutions = Screen.resolutions;
        
        SetResolutionsForDropDown();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, bool> pair in data.coinsCollected)
        {
            if(pair.Value)
            {
                GameManager.GetInstance().coinCount++;
            }
        }
        foreach (KeyValuePair<string, bool> pair in data.crystalsCollected)
        {
            if (pair.Value)
            {
                GameManager.GetInstance().crystalCount++;
            }
        }
    }

    public void SaveData(GameData data)
    {
        // nothing needs to be saved
    }

    // ------------------------------------------------------------------ PAUSE MENU ----------------------------------------
    public void PauseMenu()
    {
        optionsPanel.SetActive(false);
        mainmenuPanel.SetActive(false);
        savePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        mainmenuPanel.SetActive(false);
        savePanel.SetActive(false);
        GameManager.GetInstance().isPaused = false;
    }

    public void Options()
    {
        mainmenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        savePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("bgmVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }
    
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetResolutionsForDropDown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    

    // ----------------------------------------------------- MAIN MENU -----------------------------------------

    public void Mainmenu()
    {
        optionsPanel.SetActive(false);
        mainmenuPanel.SetActive(true);
    }

    public void MainmenuOptions()
    {
        mainmenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // ------------------------------------------------------ SAVE MENU -------------------------------------------

    public void SaveMenu()
    {
        savePanel.SetActive(true);
        GameManager.GetInstance().isPaused = true;
    }

    // ----------------------------------------------------- HUD ----------------------------------------------------

    public void UpdateCoins(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }

    public void UpdateCrystals(int crystalAmount)
    {
        crystalText.text = crystalAmount.ToString();
    }

    public void UpdateHealth(int healthAmount)
    {
        for(int i = 0; i < healthArray.Length; i++)
        {
            if(i < healthAmount)
            {
                healthArray[i].enabled = true;
            }
            else
            {
                healthArray[i].enabled = false;
            }
        }
    }

    // ----------------------------------------------------------- GAME OVER -----------------------------------------------

    public void GameOverScreen()
    {
        gameOverGroup.SetActive(true);
    }
}
