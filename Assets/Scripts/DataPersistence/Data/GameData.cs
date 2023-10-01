using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int health;
    public Vector3 playerPos;
    public int coins;
    public int crystals;

    public GameData()
    {
        this.health = 5;
        playerPos = new Vector3(-13.05f, 0.08f, 0);
        this.coins = 0;
        this.crystals = 0;
    }
}
