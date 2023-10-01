using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.GetInstance().crystalCount++;
            UIManager.GetInstance().UpdateCrystals(GameManager.GetInstance().crystalCount);
            Destroy(this.gameObject);
        }
    }

    
}
