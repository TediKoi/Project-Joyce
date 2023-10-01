using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.GetInstance().coinCount++;
            UIManager.GetInstance().UpdateCoins(GameManager.GetInstance().coinCount);
            Destroy(this.gameObject);
        }
    }

    
}
