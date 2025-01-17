using UnityEngine;
using UnityEngine.UI;

public class StampGame : MonoBehaviour
{
    public Button paperButton; 
    public Image paperImage;  

    public Color redColor = Color.red;
    public Color blueColor = Color.blue;

    private int score = 0;
    private int lives = 3;
    private string currentColor;

    void Start()
    {

        paperButton.onClick.AddListener(() => HandleLeftClick());
        RandomizePaperColor();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }

    void RandomizePaperColor()
    {
        if (Random.value > 0.5f)
        {
            paperImage.color = redColor;
            currentColor = "red";
        }
        else
        {
            paperImage.color = blueColor;
            currentColor = "blue";
        }
    }

    void HandleLeftClick()
    {
        if (currentColor == "red")
        {
            score++;
            Debug.Log($"Correct! Score: {score}");
            CheckScore();
        }
        else
        {
            lives--;
            Debug.Log($"Wrong! Lives left: {lives}");
            CheckGameOver();
        }
        RandomizePaperColor(); 
    }

    void HandleRightClick()
    {
        if (currentColor == "blue")
        {
            score++;
            Debug.Log($"Correct! Score: {score}");
            CheckScore();
        }
        else
        {
            lives--;
            Debug.Log($"Wrong! Lives left: {lives}");
            CheckGameOver();
        }
        RandomizePaperColor();
    }

    void CheckScore()
    {
        if (score % 10 == 0) 
        {
            FindObjectOfType<EmotionManager>().AdjustEmotion("happy", 2f); 
            Debug.Log("Happy emotion increased!");
        }
    }

    void CheckGameOver()
    {
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
