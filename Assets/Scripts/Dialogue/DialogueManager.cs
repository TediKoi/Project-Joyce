using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator choiceAnimator;
    [SerializeField]
    private GameObject continueButton;
    

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private List<Choice> currentChoices;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one dialogue manager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        
        //gets all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if(!dialogueIsPlaying)
        {
            return;
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        
        animator.SetBool("isOpen", true);
        
        

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        animator.SetBool("isOpen", false);
        TimelineEvents.GetInstance().ExitTimeline();

        StartCoroutine(DialogueIdle());
        

        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            
            continueButton.SetActive(true);
            //set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //display the choices when available
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public IEnumerator DialogueIdle()
    {
        
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("isIdle");
        choiceAnimator.SetTrigger("isChoiceIdle");


    }

    private void DisplayChoices()
    {
        currentChoices = currentStory.currentChoices;

        //checks to make sure our UI has enough buttons for the choices we have
        if(currentChoices.Count > choices.Length)
        {
            print("More choices were given than the UI can support. Number of choices given: " + currentChoices);
        }

        int index = 0;
        
        //enable and start the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            continueButton.SetActive(false);
            animator.SetBool("isMakingChoice", true);
            choiceAnimator.SetBool("isChoiceOpen", true);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure they're hidden
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        animator.SetBool("isMakingChoice", false);
        choiceAnimator.SetBool("isChoiceOpen", false);
        StartCoroutine(DialogueIdle());
        ContinueStory();
    }
}
