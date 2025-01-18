using UnityEngine;
using UnityEngine.UI;

public class StampGame : MonoBehaviour
{
    public Button paperButton;
    public Image paperImage;
    public Scrollbar targetAreaScrollbar;
    public Scrollbar movingBarScrollbar;

    public Color redColor = Color.red;
    public Color blueColor = Color.blue;

    public float barSpeed = 0.2f;
    private bool movingRight = true;
    private string currentColor;

    private int score = 0;
    private int lives = 5;

    public float shrinkAmount = 0.1f;
    public float minTargetWidth = 0.2f;

    private RectTransform targetHandle;

    void Start()
    {
       
        targetHandle = targetAreaScrollbar.handleRect;

        paperButton.onClick.AddListener(() => HandleLeftClick());
        RandomizePaperColor();
    }

    void Update()
    {
        MoveBar();

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }

    void MoveBar()
    {
        float step = barSpeed * Time.deltaTime;

        if (movingRight)
        {
            movingBarScrollbar.value += step;
            if (movingBarScrollbar.value >= 1f) movingRight = false;
        }
        else
        {
            movingBarScrollbar.value -= step;
            if (movingBarScrollbar.value <= 0f) movingRight = true;
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

    void RandomizeTargetPosition()
    {
        float newPosition = Random.Range(0f, 1f);
        targetAreaScrollbar.value = newPosition;
    }

    void HandleLeftClick()
    {
        if (currentColor == "red" && IsOnTarget())
        {
            score++;
            Debug.Log($"Correct! Score: {score}");
            CheckScore();
        }
        else
        {
            lives--;
            ShrinkTargetArea();
            Debug.Log($"Wrong! Lives left: {lives}");
            CheckGameOver();
        }

        RandomizeTargetPosition();
        RandomizePaperColor();
    }

    void HandleRightClick()
    {
        if (currentColor == "blue" && IsOnTarget())
        {
            score++;
            Debug.Log($"Correct! Score: {score}");
            CheckScore();
        }
        else
        {
            lives--;
            ShrinkTargetArea();
            Debug.Log($"Wrong! Lives left: {lives}");
            CheckGameOver();
        }

        RandomizeTargetPosition();
        RandomizePaperColor();
    }

    bool IsOnTarget()
    {
        float distance = Mathf.Abs(movingBarScrollbar.value - targetAreaScrollbar.value);
        float targetRange = targetHandle.sizeDelta.x / targetAreaScrollbar.GetComponent<RectTransform>().sizeDelta.x;

        return distance <= targetRange;
    }


    void ShrinkTargetArea()
    {
        if (targetAreaScrollbar.size > minTargetWidth)
        {
            targetAreaScrollbar.size = Mathf.Max(targetAreaScrollbar.size - shrinkAmount, minTargetWidth);

            Debug.Log($"Target area size reduced to: {targetAreaScrollbar.size}");
        }
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
