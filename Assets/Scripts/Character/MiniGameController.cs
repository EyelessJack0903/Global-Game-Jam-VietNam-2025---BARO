using System.Collections;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public WallClock wallClock;
    public GameObject[] hideForMinigame;
    public GameObject minigameBedPrefab;
    public GameObject wcPanel;
    public GameObject minigameComputerPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Map home ----
        // Kiểm tra nếu đối tượng va chạm có tên là "Bed"
        if (collision.gameObject.name == "Bed")
        {
            // Ẩn tất cả các GameObject trong mảng hideForMinigame
            foreach (GameObject obj in hideForMinigame)
            {
                if (obj != null)
                {
                    // Kiểm tra nếu obj là "character" bằng cách sử dụng tag
                    if (obj.CompareTag("Character"))
                    {
                        // Tìm script MoveToMouse trên obj và vô hiệu hóa nó
                        MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                        if (moveScript != null)
                        {
                            moveScript.enabled = false;
                        }
                    }
                    else
                    {
                        // Ẩn obj nếu không phải là "character"
                        obj.SetActive(false);
                    }
                }
            }

            // Sinh ra prefab ở vị trí gốc (0, 0, 0) hoặc vị trí mong muốn
            if (minigameBedPrefab != null)
            {
                Instantiate(minigameBedPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Prefab chưa được gán!");
            }
        }

        // Kiểm tra nếu đối tượng va chạm có tên là "WC"
        if (collision.gameObject.name == "WC")
        {
            // Ẩn tất cả các GameObject trong mảng hideForMinigame
            foreach (GameObject obj in hideForMinigame)
            {
                if (obj != null)
                {
                    // Kiểm tra nếu obj là "character" bằng cách sử dụng tag
                    if (obj.CompareTag("Character"))
                    {
                        // Tìm script MoveToMouse trên obj và vô hiệu hóa nó
                        MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                        if (moveScript != null)
                        {
                            moveScript.enabled = false;
                        }
                    }
                    else
                    {
                        // Ẩn obj nếu không phải là "character"
                        obj.SetActive(false);
                    }
                }
            }


            // Hiện wcPanel
            if (wcPanel != null)
            {
                wallClock.SetTimeScale(5);
                wcPanel.SetActive(true);

                // Gọi Coroutine để ẩn wcPanel sau 3 giây và hiển thị lại các GameObject
                StartCoroutine(HandleWCPanel());
            }
            else
            {
                Debug.LogWarning("wcPanel chưa được gán!");
            }
        }

        // Map Company ----
        if (collision.gameObject.name == "Computer")
        {
            // Ẩn tất cả các GameObject trong mảng hideForMinigame
            foreach (GameObject obj in hideForMinigame)
            {
                if (obj != null)
                {
                    // Kiểm tra nếu obj là "character" bằng cách sử dụng tag
                    if (obj.CompareTag("Character"))
                    {
                        // Tìm script MoveToMouse trên obj và vô hiệu hóa nó
                        MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                        if (moveScript != null)
                        {
                            moveScript.enabled = false;
                        }
                    }
                    else
                    {
                        // Ẩn obj nếu không phải là "character"
                        obj.SetActive(false);
                    }
                }
            }

            // Sinh ra prefab ở vị trí gốc (0, 0, 0) hoặc vị trí mong muốn
            if (minigameComputerPrefab != null)
            {
                Instantiate(minigameComputerPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Prefab chưa được gán!");
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
                // Kiểm tra nếu obj là "character"
                if (obj.CompareTag("Character"))
                {
                    // Bật lại script MoveToMouse nếu nó bị vô hiệu hóa
                    MoveToMouse moveScript = obj.GetComponent<MoveToMouse>();
                    if (moveScript != null)
                    {
                        moveScript.enabled = true;
                    }

                    // Di chuyển nhân vật vào trung tâm (tọa độ Vector3.zero hoặc vị trí mong muốn)
                    obj.transform.position = Vector3.zero;
                }
                else
                {
                    // Bật lại các obj khác
                    obj.SetActive(true);
                }
            }
        }
    }
}
