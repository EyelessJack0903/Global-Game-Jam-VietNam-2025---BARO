using System.Collections;
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

    private void Update()
    {
        // Kiểm tra nếu nhấn phím ESC và Computer đang bật
        if (Input.GetKeyDown(KeyCode.Escape) && isComputerActive)
        {
            CloseComputer();
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
}
