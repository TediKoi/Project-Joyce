using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Coin : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public bool collected;

    [ContextMenu("Generate unique id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if(collected)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if(data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collected = true;
            GameManager.GetInstance().coinCount++;
            UIManager.GetInstance().UpdateCoins(GameManager.GetInstance().coinCount);
            this.gameObject.SetActive(false);
        }
    }
}
