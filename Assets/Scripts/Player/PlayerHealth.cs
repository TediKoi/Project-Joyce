using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    [Header("Health")]
    [SerializeField]
    public int currentHealth = 0;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private ParticleSystem blood;

    private static PlayerHealth instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.GetInstance().UpdateHealth(currentHealth);
    }

    public static PlayerHealth GetInstance()
    {
        return instance;
    }

    public void TakeDmg(int dmg)
    {
        
        currentHealth -= dmg;
        playerMovement.animator.SetTrigger("isHurt");
        AudioManager.GetInstance().PlaySFX(7);
        blood.Play();
        if (currentHealth <= 0)
        {
            PlayerDead();
        }
        
    }

    public void PlayerDead()
    {
        Debug.Log("Player Dead");
        AudioManager.GetInstance().PlaySFX(8);
        playerMovement.animator.SetTrigger("isDead");
    }

    public void GameOver()
    {
        GameManager.GetInstance().gameOver = true;

    }

    public void LoadData(GameData data)
    {
        this.currentHealth = data.health;
    }

    public void SaveData(GameData data)
    {
        data.health = this.currentHealth;
    }
}
