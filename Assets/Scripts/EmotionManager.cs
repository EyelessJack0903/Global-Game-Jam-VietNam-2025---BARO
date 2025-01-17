using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public float happy;
    public float sad;
    public float angry;
    public float scared;

    public float maxEmotionValue = 10f;

    void Update()
    {
        CheckDominantEmotion();

        Debug.Log($"Happy: {happy}, Sad: {sad}, Angry: {angry}, Scared: {scared}");
    }

    void CheckDominantEmotion()
    {
        if (happy >= maxEmotionValue)
        {
            happy = maxEmotionValue; 
            Debug.Log("Happy State");
        }
        else if (sad >= maxEmotionValue)
        {
            sad = maxEmotionValue;
            Debug.Log("Sad State");
        }
        else if (angry >= maxEmotionValue)
        {
            angry = maxEmotionValue;
            Debug.Log("Angry State");
        }
        else if (scared >= maxEmotionValue)
        {
            scared = maxEmotionValue;
            Debug.Log("Scared State");
        }
    }

    public void AdjustEmotion(string emotion, float amount)
    {
        switch (emotion.ToLower())
        {
            case "happy":
                happy = Mathf.Min(maxEmotionValue, happy + amount);
                sad = Mathf.Max(0, sad - 1.5f * amount);
                angry = Mathf.Max(0, angry - 1.5f * amount);
                scared = Mathf.Max(0, scared - 1.5f * amount);
                break;
            case "sad":
                sad = Mathf.Min(maxEmotionValue, sad + amount);
                happy = Mathf.Max(0, happy - amount);
                break;
            case "angry":
                angry = Mathf.Min(maxEmotionValue, angry + amount);
                happy = Mathf.Max(0, happy - amount);
                break;
            case "scared":
                scared = Mathf.Min(maxEmotionValue, scared + amount);
                happy = Mathf.Max(0, happy - amount);
                break;
            default:
                Debug.LogWarning("Emotion not recognized: " + emotion);
                break;
        }

        happy = Mathf.Max(0, happy);
        sad = Mathf.Max(0, sad);
        angry = Mathf.Max(0, angry);
        scared = Mathf.Max(0, scared);
    }
}
