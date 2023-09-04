using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private List<string> dialogue;
    [SerializeField]
    private float wordSpeed = 0.05f;
    [SerializeField]
    private bool playerClose;
    [SerializeField]
    private int index;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject choice1Button;
    [SerializeField]
    private GameObject choice2Button;

    // Start is called before the first frame update
    void Start()
    {
        Dialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(playerClose)
        {
            if (context.performed)
            {
                if(dialoguePanel.activeInHierarchy)
                {
                    ResetText();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    animator.SetBool("isOpen", true);
                    
                    StartCoroutine(Typing());
                }
                
            }
        }
        
    }

    public void ResetText()
    {
        dialogueText.text = "";
        index = 0;
        animator.SetBool("isOpen", false);
        animator.SetTrigger("isClosed");
        StartCoroutine(PanelOff());
    }

    IEnumerator PanelOff()
    {
        yield return new WaitForSeconds(1);
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);

            Choice();
            
        }
        
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);

        if(index < dialogue.Count - 1) 
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
            ResetText();
        }
    }

    public void Choice()
    {
        if (index == 1 && dialogueText.text == dialogue[index])
        {
            choice1Button.SetActive(true);
            choice2Button.SetActive(true);

            
        }
        else
        {
            if (dialogueText.text == dialogue[index])
            {
                continueButton.SetActive(true);
            }
        }
    }

    public void Dialogue()
    {
        dialogue.Add("Hey Joyce!"); //0
        dialogue.Add("We were all waiting for you! What took you so long?"); //1
        dialogue.Add("Ohh.. Well everyones waiting for you at the entrance. See you there!");//2

    }
    
}
