using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int health;
    public Vector3 playerPos;
    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, bool> crystalsCollected;

    public GameData()
    {
        this.health = 3;
        playerPos = new Vector3(-13.05f, 0.08f, 0);
        coinsCollected = new SerializableDictionary<string, bool>();
        crystalsCollected = new SerializableDictionary<string, bool>();
    }
}
