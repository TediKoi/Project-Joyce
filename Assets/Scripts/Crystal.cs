using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public bool collected;

    [SerializeField] private ParticleSystem boomParticle;

    [ContextMenu("Generate unique id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Start()
    {

    }

    public void LoadData(GameData data)
    {
        data.crystalsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.crystalsCollected.ContainsKey(id))
        {
            data.crystalsCollected.Remove(id);
        }
        data.crystalsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collected = true;
            GameManager.GetInstance().crystalCount++;
            UIManager.GetInstance().UpdateCrystals(GameManager.GetInstance().crystalCount);
            AudioManager.GetInstance().PlaySFX(10);
            boomParticle.Play();
            this.gameObject.SetActive(false);
        }
    }

    
}
