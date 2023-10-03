using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool youWin;
    public bool isPaused;

    public int currentLevel;
    public int currentHealth;
    public int coinCount;
    public int crystalCount;
    public PlayerMovement player;

    private static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        UpdateCoinsText();
        UpdateCrystalsText();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void UpdateCoinsText()
    {
        UIManager.GetInstance().UpdateCoins(coinCount);
    }

    public void UpdateCrystalsText()
    {
        UIManager.GetInstance().UpdateCrystals(crystalCount);
    }

    
}
