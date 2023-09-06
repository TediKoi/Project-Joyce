using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CherryNPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private Image dialoguePanelImage;
    [SerializeField]
    private Image profilePic;
    [SerializeField]
    private TMP_Text profileName;
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

    [Header("Customizable")]
    [SerializeField]
    private Color myColor;
    [SerializeField]
    private Sprite myProfilePic;
    [SerializeField]
    private string myName;

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
        if (playerClose)
        {
            if (context.performed)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    ResetText();
                }
                else
                {
                    dialoguePanelImage.color = myColor;
                    profilePic.sprite = myProfilePic;
                    profileName.text = myName;
                    dialoguePanel.SetActive(true);
                    animator.SetBool("isOpen", true);

                    StartCoroutine(Typing());
                }

            }
        }

    }

    public void ResetText() //this gets rid of the dialogue text and panel
    {
        dialogueText.text = "";
        index = 0;
        animator.SetBool("isOpen", false);
        animator.SetTrigger("isClosed");
        StartCoroutine(PanelOff());
    }

    IEnumerator PanelOff() //this is to turn off the panel after a short period of time
    {
        yield return new WaitForSeconds(1);
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() //this does the typing animation for the dialogue text
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);

            Choice();

        }

    }

    public void NextLine() //this gets the next line of the dialogue
    {
        continueButton.SetActive(false);
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);

        if (index < dialogue.Count - 1)
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

    public void Choice1()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);

        dialogue.Add("Uber was late? Damn, you need to get a car ASAP. Anyways, let's go, everyone is waiting for us. ");

        if (index < dialogue.Count - 1)
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

    public void Choice2()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);

        dialogue.Add("Wow.. If you didn't get paid overtime, I would quit. Anyways, let's go, everyone is waiting for us. ");

        if (index < dialogue.Count - 1)
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
        if (other.CompareTag("Player"))
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

    public void Choice() //this determines whether the buttons should be a continue button or choice buttons
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

    public void Dialogue() //this is the beginning dialogue script before any choices are made
    {
        dialogue.Add("Hey Joyce!"); //0
        dialogue.Add("We were all waiting for you! What took you so long?"); //1


    }

}
