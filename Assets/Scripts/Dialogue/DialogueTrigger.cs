using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cues")]
    [SerializeField]
    private GameObject visualCue;

    private bool isPlayerClose;
    public PlayerInteract playerInteract;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    private void Awake()
    {
        isPlayerClose = false;
        visualCue.SetActive(false);
        
    }

    private void Update()
    {
        if(isPlayerClose && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if(playerInteract.isInteracting == true)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerClose = false;
        }
    }


}
