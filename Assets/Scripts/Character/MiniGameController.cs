using System.Collections;
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

    private bool isComputerActive = false; // Biến kiểm soát trạng thái của Computer
    private GameObject activeComputerInstance; // Lưu trữ instance của Computer khi được tạo
    public TextMeshProUGUI winTextUI;
    private bool isMinigameActive = false;

    private void Start()
    {
        // winTextUI.gameObject.SetActive(false);
    }

    private void Update()
    {
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
            isMinigameActive = true;
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

                    // BoxCollider2D coll = obj.GetComponent<BoxCollider2D>();
                    // if (coll != null)
                    // {
                    //     coll.enabled = false;
                    // }
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

                    // BoxCollider2D coll = obj.GetComponent<BoxCollider2D>();
                    // if (coll != null)
                    // {
                    //     coll.enabled = true;
                    // }
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

    private IEnumerator HandleWCPanel()
    {
        // Chờ 3 giây
        yield return new WaitForSeconds(3f);
        wallClock.SetTimeScale(1);

        // Ẩn wcPanel
        if (wcPanel != null)
        {
            wcPanel.SetActive(false);
        }

        // Hiển thị lại các GameObject trong mảng hideForMinigame
        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
            {
                if (obj.CompareTag("Character"))
                {
                    // Bật lại script MoveToMouse nếu nó bị vô hiệu hóa
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }

                    // Đặt nhân vật tại vị trí chính giữa màn hình
                    obj.transform.position = new Vector3(0, -1, 0);
                }
                else
                {
                    // Hiển thị lại các obj khác
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
