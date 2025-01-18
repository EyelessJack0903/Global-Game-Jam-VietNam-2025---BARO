using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMesh Pro namespace

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI speakerNameText; 
    public Button positiveChoiceButton;
    public Button negativeChoiceButton;
    public Dialogue currentDialogue;

    void Start()
    {
        ShowDialogue(currentDialogue);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !currentDialogue.isChoice)
        {
            ShowChoices();
        }
    }

    void ShowDialogue(Dialogue dialogue)
    {
        dialogueText.text = dialogue.dialogueText;
        speakerNameText.text = dialogue.speakerName;
        dialogueText.gameObject.SetActive(true);
        speakerNameText.gameObject.SetActive(true);

        if (!dialogue.isChoice)
        {
            positiveChoiceButton.gameObject.SetActive(false);
            negativeChoiceButton.gameObject.SetActive(false);
        }
    }

    void ShowChoices()
    {
        dialogueText.gameObject.SetActive(false); 
        speakerNameText.gameObject.SetActive(false); 

        if (currentDialogue.nextDialogues.Count >= 2)
        {
            Dialogue positiveDialogue = currentDialogue.nextDialogues[0];
            Dialogue negativeDialogue = currentDialogue.nextDialogues[1];

            positiveChoiceButton.gameObject.SetActive(true);
            negativeChoiceButton.gameObject.SetActive(true);

            positiveChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = positiveDialogue.dialogueText;
            negativeChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = negativeDialogue.dialogueText;

            positiveChoiceButton.onClick.RemoveAllListeners();
            negativeChoiceButton.onClick.RemoveAllListeners();

            positiveChoiceButton.onClick.AddListener(() => ChooseDialogue(positiveDialogue));
            negativeChoiceButton.onClick.AddListener(() => ChooseDialogue(negativeDialogue));
        }
    }

    void ChooseDialogue(Dialogue chosenDialogue)
    {
        positiveChoiceButton.gameObject.SetActive(false);
        negativeChoiceButton.gameObject.SetActive(false);

        ShowDialogue(chosenDialogue);

        StartCoroutine(WaitAndCheckNextDialogue(chosenDialogue));
    }

    IEnumerator WaitAndCheckNextDialogue(Dialogue dialogue)
    {
        yield return new WaitForSeconds(dialogue.delayTime); 

        if (dialogue.nextDialogues.Count == 1)
        {
            currentDialogue = dialogue.nextDialogues[0]; 
            ShowDialogue(currentDialogue);
        }
    }
}
