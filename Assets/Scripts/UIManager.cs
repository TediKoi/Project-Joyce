using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject mainmenuPanel;

    public AudioMixer audioMixer;

    private static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    public void PauseMenu()
    {
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        GameManager.GetInstance().isPaused = false;
    }

    public void Options()
    {
        mainmenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
    
}
