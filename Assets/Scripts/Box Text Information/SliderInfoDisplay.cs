using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderInfoDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text infoText; // Text UI để hiển thị thông tin
    private Slider slider; // Tham chiếu đến Slider

    void Start()
    {
        slider = GetComponent<Slider>(); // Lấy Slider gắn vào GameObject
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false); // Ẩn thông tin khi khởi động
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Hiển thị thông tin khi chuột di vào
        if (infoText != null && slider != null)
        {
            infoText.text = $"Tên: {gameObject.name}\n" +
                            $"Giá trị hiện tại: {slider.value:F2}\n" +
                            $"Giá trị tối đa: {slider.maxValue}";
            infoText.gameObject.SetActive(true);
            UpdateInfoTextPosition();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Ẩn thông tin khi chuột rời khỏi
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
            infoText.text = "";
        }
    }

    void Update()
    {
        if (infoText != null && infoText.gameObject.activeSelf)
        {
            UpdateInfoTextPosition();
        }
    }

    private void UpdateInfoTextPosition()
    {
        // Cập nhật vị trí Text theo vị trí chuột
        Vector3 mousePosition = Input.mousePosition;
        infoText.transform.position = mousePosition + new Vector3(10f, -10f, 0f); // Điều chỉnh vị trí text
    }
}
