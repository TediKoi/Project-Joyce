using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    private bool isPlayerClose;
    [SerializeField] private GameObject visualCue;
    [SerializeField] private FadeLevelChanger changer;
    [SerializeField] private int loadToLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerClose)
        {
            if(PlayerInteract.GetInstance().isInteracting == true)
            {
                changer.FadeToLevel(loadToLevel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerClose = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerClose = false;
            visualCue.SetActive(false);
        }
    }
}
