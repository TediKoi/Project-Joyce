using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteractable : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private bool isPlayerClose;
    [SerializeField] private GameObject visualCue;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerClose = false;
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerClose)
        {
            if (playerInteract.isInteracting == true)
            {
                UIManager.GetInstance().SaveMenu();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerClose = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerClose = false;
            visualCue.SetActive(false);
        }
    }
}
