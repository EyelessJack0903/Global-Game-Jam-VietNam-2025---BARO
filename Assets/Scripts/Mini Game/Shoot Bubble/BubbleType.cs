using UnityEngine;

public class BubbleType : MonoBehaviour
{
    public enum BubbleEmotion
    {
        Sad,
        Happy,
        Angry,
        Fear,
        Tired
    }

    [Header("Emotion Bubble")]
    public BubbleEmotion bubble;
}
