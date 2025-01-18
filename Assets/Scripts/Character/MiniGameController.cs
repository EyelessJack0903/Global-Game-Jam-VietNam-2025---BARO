using System.Collections;
using TMPro;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public WallClock wallClock;
    public GameObject[] hideForMinigame;
    public GameObject minigameBedPrefab;
    public GameObject wcPanel;
<<<<<<< HEAD
    public GameObject minigameComputerPrefab;

    private bool isComputerActive = false; // Biến kiểm soát trạng thái của Computer
    private GameObject activeComputerInstance; // Lưu trữ instance của Computer khi được tạo

    private void Update()
    {
        // Kiểm tra nếu nhấn phím ESC và Computer đang bật
        if (Input.GetKeyDown(KeyCode.Escape) && isComputerActive)
        {
            CloseComputer();
=======
    public TextMeshProUGUI winTextUI;

    private bool isMinigameActive = false;

    private void Start()
    {
        winTextUI.gameObject.SetActive(false);
    }
    void Update()
    {
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
>>>>>>> 3ad9ac6e88c5ef34657dc7185521c4c855ae1ca6
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
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
    }

    private void HandleMinigame(GameObject prefab)
    {
        foreach (GameObject obj in hideForMinigame)
        {
            if (obj != null)
=======
        // Kiểm tra nếu đối tượng va chạm có tên là "Bed"
        if (collision.gameObject.name == "Bed")
        {
            isMinigameActive = true;
            // Ẩn tất cả các GameObject trong mảng hideForMinigame
            foreach (GameObject obj in hideForMinigame)
>>>>>>> 3ad9ac6e88c5ef34657dc7185521c4c855ae1ca6
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

    private void CloseComputer()
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

        winTextUI.gameObject.SetActive(false);
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            Destroy(bubble);
        }

        StartCoroutine(HandleWCPanel());
    }

}
