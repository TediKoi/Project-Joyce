using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Dialogue Panel")]
    [SerializeField] private Image panel;
    [SerializeField] private Image panelProfileImage;
    [SerializeField] private TextMeshProUGUI panelProfileName;
    [SerializeField] private Sprite profileImage;
    [SerializeField] private Color panelColor;
    [SerializeField] private string profileName;

    private void Awake()
    {
        isPlayerClose = false;
        visualCue.SetActive(false);
        
    }

    private void Update()
    {
        if(isPlayerClose && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            panel.color = panelColor;
            panelProfileImage.sprite = profileImage;
            panelProfileName.text = profileName;
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
