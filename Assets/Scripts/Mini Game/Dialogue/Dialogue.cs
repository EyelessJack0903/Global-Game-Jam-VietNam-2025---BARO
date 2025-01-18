using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueCharacter", order = 1)]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogueText;

    public bool isPositive; // tích cực (true), tiêu cực (false)
    public string speakerName;
    public List<Dialogue> nextDialogues;
    public bool isChoice;
    public float delayTime = 2f;
}
