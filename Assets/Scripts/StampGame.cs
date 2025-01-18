using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public GameObject gameOverPanel;
    public static bool isGameOver = false;
    public static bool isWinner = false;

    private EmotionManager emotionManager;

    void Start()
    {
        emotionManager = FindFirstObjectByType<EmotionManager>();

        targetHandle = targetAreaScrollbar.handleRect;

        paperButton.onClick.AddListener(() => HandleLeftClick());
        RandomizePaperColor();

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        MoveBar();

        if (Input.GetMouseButtonDown(1))
        {
            if (IsPointerOverUIElement(paperButton))
            {
                HandleRightClick();
            }
        }
    }

    bool IsPointerOverUIElement(Button button)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == button.gameObject)
            {
                return true;
            }
        }

        return false;
    }


    bool IsPointerOverPaperButton()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransform paperButtonRect = paperButton.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(paperButtonRect, mousePosition, Camera.main);
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
        float currentPosition = targetAreaScrollbar.value;
        float newPosition;

        do
        {
            newPosition = Random.Range(0f, 1f);
        }
        while (Mathf.Abs(newPosition - currentPosition) < 0.3f);
        targetAreaScrollbar.value = newPosition;
    }


    void HandleLeftClick()
    {
        if (currentColor == "red" && IsOnTarget())
        {
            score++;
            Debug.Log($"Correct! Score: {score}");
            emotionManager.AdjustEmotion("happy", 0.2f);
            CheckScore();
        }
        else
        {
            lives--;
            emotionManager.AdjustEmotion("scared", 0.1f);
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

        float collisionRange = 0.1f;

        return distance <= collisionRange;
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
            isWinner = true;
        }
    }

    void CheckGameOver()
    {
        if (lives <= 0)
        {
            Debug.Log("Game Over!");

            isGameOver = true;

            gameOverPanel.SetActive(true);

            FindObjectOfType<EmotionManager>().AdjustEmotion("scared", 3f);
        }
    }
}