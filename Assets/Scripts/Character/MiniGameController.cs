using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public WallClock wallClock;
    public GameObject[] hideForMinigame;
    public GameObject minigameBedPrefab;
    public GameObject wcPanel;
    public GameObject minigameComputerPrefab;
    public GameObject minigamePrinterPrefab;
    public GameObject minigameMoodPrefab;
    public GameObject minigameDialoguePrefab;

    private bool isComputerActive = false; // Biến kiểm soát trạng thái của Computer
    private GameObject activeComputerInstance; // Lưu trữ instance của Computer khi được tạo
    private bool isMinigameActive;
    public TextMeshProUGUI winTextUI;
    
    [HideInInspector] public bool isFinishComputerMinigame;
    private bool isFinishPrinterMinigame;
    [HideInInspector] public bool canPlayDialogueMinigame;
    private bool isFinishMoodMinigame;
    private void Start() {
        isFinishComputerMinigame = false;
        isFinishPrinterMinigame = false;
        canPlayDialogueMinigame = false;
    }

    private void Update()
    {
        if (isFinishPrinterMinigame && isFinishComputerMinigame){
            HandleMinigame(minigameMoodPrefab);
            isFinishComputerMinigame = false;
            isFinishPrinterMinigame = false;
            isFinishMoodMinigame = true;
        }

        if (GameController.m_isWinner)
        {
            CloseMood();
        }

        if (GameController.m_isGameover)
        {
            CloseMood();
        }

        if (StampGame.isGameOver)
        {
            isFinishPrinterMinigame = true;
            ClosePrince();
        }

        if (StampGame.isWinner)
        {
            isFinishPrinterMinigame = true;
            ClosePrince();
        }

        if (canPlayDialogueMinigame && isFinishMoodMinigame)
        {
            HandleMinigame(minigameDialoguePrefab); 
            canPlayDialogueMinigame = false;
        }

        // Kiểm tra nếu nhấn phím ESC và Computer đang bật
        if (Input.GetKeyDown(KeyCode.Escape) && isComputerActive)
        {
            CloseComputer();
        }
        if (isMinigameActive)
        {
            // Tìm kiếm các GameObject có layer "Happy"
            int happyLayer = LayerMask.NameToLayer("Happy");
            GameObject[] happyObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in happyObjects)
            {
                if (obj.layer == happyLayer)
                {
                    StartCoroutine(ShowWinScreen());
                    return;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Map home ----
        if (collision.gameObject.name == "Bed")
        {
            HandleMinigame(minigameBedPrefab);
        }

        if (collision.gameObject.name == "WC")
        {
            HandleWC();
        }

        // Map Company ----
        if (collision.gameObject.name == "Computer")
        {
            HandleMinigame(minigameComputerPrefab);
            isComputerActive = true; // Đánh dấu trạng thái Computer đang bật
        }
        if (collision.gameObject.name == "Printer")
        {
            HandleMinigame(minigamePrinterPrefab);
        }
    }

    private void HandleMinigame(GameObject prefab)
    {
        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = false;
                    }
                }
                else
                {
                    obj.SetActive(false);
                }
            }
        }

        if (prefab != null)
        {
            activeComputerInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab chưa được gán!");
        }
    }

    private void HandleWC()
    {
        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = false;
                    }
                }
                else
                {
                    obj.SetActive(false);
                }
            }
        }

        if (wcPanel != null)
        {
            wallClock.SetTimeScale(5);
            wcPanel.SetActive(true);
            StartCoroutine(HandleWCPanel());
        }
        else
        {
            Debug.LogWarning("wcPanel chưa được gán!");
        }
    }

    public void CloseComputer()
    {
        isComputerActive = false; // Đặt lại trạng thái
        if (activeComputerInstance != null)
        {
            Destroy(activeComputerInstance); // Xóa instance của Computer
        }

        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }
                }
                obj.SetActive(true); // Bật lại các obj khác
            }
        }
    }

    public void ClosePrince()
    {
        if (activeComputerInstance != null)
        {
            Destroy(activeComputerInstance); 
        }

        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }
                }
                obj.SetActive(true); // Bật lại các obj khác
            }
        }
        StampGame.isGameOver = false;
        StampGame.isWinner = false;
    }

    public void CloseMood()
    {
        if (activeComputerInstance != null)
        {
            Destroy(activeComputerInstance);
        }

        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }
                }
                obj.SetActive(true); // Bật lại các obj khác
            }
        }
        isFinishMoodMinigame = true;
        GameController.m_isGameover = false;
        GameController.m_isWinner = false;
    }

    private IEnumerator HandleWCPanel()
    {
        yield return new WaitForSeconds(3f);
        wallClock.SetTimeScale(1);

        if (wcPanel != null)
        {
            wcPanel.SetActive(false);
        }

        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }
                    obj.transform.position = Vector3.zero;
                }
                else
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    private IEnumerator ShowWinScreen()
    {
        isMinigameActive = false;
        // Hiển thị dòng chữ "Win"
        if (winTextUI != null)
        {
            winTextUI.text = "WIN";
            winTextUI.gameObject.SetActive(true);
        }
        //GameObject[] bubbleObjects = GameObject.FindGameObjectsWithTag("Bubble");
        //foreach (GameObject bubble in bubbleObjects)
        //{
        //    Destroy(bubble);
        //}
        //foreach (GameObject obj in hideForMinigame)
        //{
        //    if (obj != null)
        //    {
        //        obj.SetActive(true);
        //        if (obj.CompareTag("Character"))
        //        {
        //            MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
        //            if (moveScript != null)
        //            {
        //                moveScript.enabled = true;
        //            }
        //        }
        //    }
        //}
        yield return new WaitForSeconds(2f);
        // winTextUI.gameObject.SetActive(false);
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            Destroy(bubble);
        }
        StartCoroutine(HandleWCPanel());
    }
}
