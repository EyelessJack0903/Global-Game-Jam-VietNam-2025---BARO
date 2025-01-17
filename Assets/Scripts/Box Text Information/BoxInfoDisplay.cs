using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxInfoDisplay : MonoBehaviour
{
    public TMP_Text infoText; // Text UI để hiển thị thông tin
    private string currentInfo = "";

    void Start()
    {
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false); // Ẩn thông tin khi khởi động
        }
    }

    void Update()
    {
        // Cập nhật vị trí Text theo vị trí chuột
        if (infoText != null && infoText.gameObject.activeSelf)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            infoText.transform.position = mousePosition + new Vector3(0.5f, -0.5f, 0f); // Dịch Text cạnh chuột
        }
    }

    void OnMouseEnter()
    {
        // Lấy thông tin Box khi chuột di vào
        if (infoText != null)
        {
            currentInfo = $"Tên: {gameObject.name}\n" +
                          $"Vị trí: {transform.position}\n" +
                          $"Kích thước: {GetComponent<Collider2D>().bounds.size}";

            infoText.text = currentInfo;
            infoText.gameObject.SetActive(true); // Hiện thông tin
        }
    }

    void OnMouseExit()
    {
        // Ẩn thông tin khi chuột rời khỏi Box
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
            infoText.text = "";
        }
    }
}
