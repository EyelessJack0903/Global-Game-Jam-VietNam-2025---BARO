using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Cần để chuyển scene

public class EmotionManager : MonoBehaviour
{
    [Header("Giá trị cảm xúc")]
    public float happy = 4f;
    public float sad = 4f;
    public float angry = 4f;
    public float scared = 4f;
    public float energy = 7f;

    [Header("Giá trị tối đa")]
    public float maxEnergyValue = 20f;
    public float maxEmotionValue = 10f;

    public Button happyButton;
    public Button sadButton;
    public Button angryButton;
    public Button scaredButton;
    public Button energyButton;

    [Header("Thanh trạng thái cảm xúc")]
    public Slider happySlider;
    public Slider sadSlider;
    public Slider angrySlider;
    public Slider scaredSlider;
    public Slider energySlider;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        UpdateEmoji(happy, sad, angry, scared, energy);

        happyButton.onClick.AddListener(() => AdjustEmotion("happy", 1f));
        sadButton.onClick.AddListener(() => AdjustEmotion("sad", 1f));
        angryButton.onClick.AddListener(() => AdjustEmotion("angry", 1f));
        scaredButton.onClick.AddListener(() => AdjustEmotion("scared", 1f));
        energyButton.onClick.AddListener(() => AdjustEnergy(1f));
    }

    void Update()
    {
        CheckDominantEmotion();

        if (happy <= 0f || sad >= maxEmotionValue || angry >= maxEmotionValue || scared >= maxEmotionValue)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void Init()
    {
        UpdateEmoji(happy, sad, angry, scared, energy);
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

        if (energy >= maxEnergyValue)
        {
            energy = maxEnergyValue;
            Debug.Log("Energy is full!");
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

        // Cập nhật Slider sau khi thay đổi giá trị emoji
        UpdateEmoji(happy, sad, angry, scared, energy);
    }

    private void UpdateEmoji(float happy, float sad, float angry, float scared, float energy)
    {
        // Đồng bộ giá trị Slider với giá trị emoji
        happySlider.value = happy;
        sadSlider.value = sad;
        angrySlider.value = angry;
        scaredSlider.value = scared;
        energySlider.value = energy;

        // Đảm bảo giá trị của Slider thay đổi đồng thời khi emoji thay đổi
        happySlider.maxValue = maxEmotionValue;
        sadSlider.maxValue = maxEmotionValue;
        angrySlider.maxValue = maxEmotionValue;
        scaredSlider.maxValue = maxEmotionValue;
        energySlider.maxValue = maxEnergyValue;
    }

    public void AdjustEnergy(float amount)
    {
        energy = Mathf.Min(maxEnergyValue, energy + amount);
        UpdateEmoji(happy, sad, angry, scared, energy);

        if (energy >= maxEnergyValue)
        {
            Debug.Log("Energy is full!");
        }
    }
}
