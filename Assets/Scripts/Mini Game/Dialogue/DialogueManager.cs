using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;
    public Button positiveChoiceButton;
    public Button negativeChoiceButton;
    public Dialogue currentDialogue;

    private EmotionManager emotionManager;
    private bool waitingForNextInput = false; // Để kiểm tra khi nào cần nhấn chuột để tiếp tục
    private bool isShowingChoices = false; // Để kiểm tra xem có đang hiển thị lựa chọn không
    private bool isWaitingForDelay = false; // Để kiểm tra trạng thái delayTime

    void Start()
    {
        emotionManager = FindFirstObjectByType<EmotionManager>();

        ShowDialogue(currentDialogue);
    }

    void Update()
    {
        if (waitingForNextInput && Input.GetMouseButtonDown(0) && !isWaitingForDelay) // Khi chờ nhấn chuột và không trong thời gian delay
        {
            waitingForNextInput = false;

            if (currentDialogue.nextDialogues.Count == 1) // Chỉ có một hội thoại tiếp theo
            {
                currentDialogue = currentDialogue.nextDialogues[0];
                ShowDialogue(currentDialogue);
            }
        }

        if (!isShowingChoices && Input.GetMouseButtonDown(0) && !currentDialogue.isChoice && !waitingForNextInput)
        {
            ShowChoices();
        }
    }

    void ShowDialogue(Dialogue dialogue)
    {
        isShowingChoices = false; // Đảm bảo không ở trạng thái hiển thị lựa chọn
        dialogueText.text = dialogue.dialogueText;
        speakerNameText.text = dialogue.speakerName;

        dialogueText.gameObject.SetActive(true);
        speakerNameText.gameObject.SetActive(true);

        positiveChoiceButton.gameObject.SetActive(false);
        negativeChoiceButton.gameObject.SetActive(false);

        // Nếu có hội thoại tiếp theo, chờ nhấn chuột
        if (dialogue.nextDialogues.Count == 1 && !dialogue.isChoice)
        {
            waitingForNextInput = true;
        }

        // Nếu là một hội thoại có lựa chọn (isChoice), đợi thời gian delay trước khi có thể tiếp tục
        if (dialogue.isChoice && dialogue.delayTime > 0)
        {
            isWaitingForDelay = true;
            StartCoroutine(WaitForDelay(dialogue.delayTime, dialogue)); // Gọi Coroutine để chờ và chuyền vào dialogue
        }
    }

    void ShowChoices()
    {
        isShowingChoices = true; // Bật trạng thái hiển thị lựa chọn
        dialogueText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);

        if (currentDialogue.nextDialogues.Count >= 2)
        {
            Dialogue positiveDialogue = currentDialogue.nextDialogues[0];
            Dialogue negativeDialogue = currentDialogue.nextDialogues[1];

            positiveChoiceButton.gameObject.SetActive(true);
            negativeChoiceButton.gameObject.SetActive(true);

            string positiveEmotionText = positiveDialogue.isPositive ? "Sad +1\nHappy +3" : "Angry +1\nScared +1";
            string negativeEmotionText = negativeDialogue.isPositive ? "Sad +1\nHappy +3" : "Angry +1\nScared +1";

            positiveChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{positiveDialogue.dialogueText}\n\n{positiveEmotionText}";
            negativeChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{negativeDialogue.dialogueText}\n\n{negativeEmotionText}";

            positiveChoiceButton.onClick.RemoveAllListeners();
            negativeChoiceButton.onClick.RemoveAllListeners();

            positiveChoiceButton.onClick.AddListener(() => ChooseDialogue(positiveDialogue));
            negativeChoiceButton.onClick.AddListener(() => ChooseDialogue(negativeDialogue));
        }
    }

    void ChooseDialogue(Dialogue chosenDialogue)
    {
        isShowingChoices = false; // Thoát trạng thái hiển thị lựa chọn

        // Điều chỉnh cảm xúc
        if (chosenDialogue.isPositive)
        {
            emotionManager.AdjustEmotion("sad", 1f);
            emotionManager.AdjustEmotion("happy", 3f);
        }
        else
        {
            emotionManager.AdjustEmotion("angry", 1f);
            emotionManager.AdjustEmotion("scared", 1f);
        }

        // Cập nhật dialogue hiện tại
        currentDialogue = chosenDialogue;

        // Hiển thị đoạn hội thoại được chọn
        ShowDialogue(chosenDialogue);
    }

    IEnumerator WaitForDelay(float delayTime, Dialogue dialogue)
    {
        yield return new WaitForSeconds(delayTime); 
        isWaitingForDelay = false; 

        if (dialogue.nextDialogues.Count == 1 || dialogue.nextDialogues.Count > 1) 
        {
            currentDialogue = dialogue.nextDialogues[0];
            ShowDialogue(currentDialogue); 
        }
        else
        {
            Debug.Log("Khong có hội thoại nữa");
        }
    }
}
