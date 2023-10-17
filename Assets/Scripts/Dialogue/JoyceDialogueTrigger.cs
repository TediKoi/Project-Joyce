using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoyceDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Panel")]
    [SerializeField] private Image panel;
    [SerializeField] private Image choicesPanel;
    [SerializeField] private Image namePanel;
    [SerializeField] private Image panelProfileImage;
    [SerializeField] private TextMeshProUGUI panelProfileName;
    [SerializeField] private Sprite profileImage;
    [SerializeField] private Color panelColor;
    [SerializeField] private string profileName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetJoyceDialoguePanel()
    {
        panel.color = panelColor;
        choicesPanel.color = panelColor;
        namePanel.color = panelColor;
        panelProfileImage.sprite = profileImage;
        panelProfileName.text = profileName;
    }
}
