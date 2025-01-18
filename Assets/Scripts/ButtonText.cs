using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private Button btn;
    public GameObject highlight;

    void Start()
    {
        btn = GetComponent<Button>();
        // Giả định rằng "Highlight" là một component con của Button
        highlight.SetActive(false); // Đảm bảo "Highlight" ẩn ban đầu
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Hiển thị Highlight khi chuột hover
        if (highlight != null)
        {
            highlight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Ẩn Highlight khi chuột không còn hover
        if (highlight != null)
        {
            highlight.SetActive(false);
        }
    }
}
