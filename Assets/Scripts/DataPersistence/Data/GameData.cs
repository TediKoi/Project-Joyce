using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int health;
    public Vector3 playerPos;

    public GameData()
    {
        this.health = 5;
        playerPos = Vector3.zero;
    }
}
