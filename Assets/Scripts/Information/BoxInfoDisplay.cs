using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxInfoDisplay : MonoBehaviour
{
    public GameObject boxInformation; 
    public TMP_Text nameText; 
    public TMP_Text infoText; 
    public string nameObject; 
    public string infoObject; 

    void Start()
    {
        if (boxInformation != null)
        {
            boxInformation.SetActive(false);
        }
    }

    void Update()
    {
        // Cập nhật vị trí Text theo vị trí chuột
        if (boxInformation != null && boxInformation.gameObject.activeSelf)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            boxInformation.transform.position = mousePosition + new Vector3(0.5f, -0.5f, 0f); // Dịch Text cạnh chuột
        }
    }

    void OnMouseEnter()
    {
        // Lấy thông tin Box khi chuột di vào
        if (boxInformation != null)
        {
            nameText.text = nameObject;
            infoText.text = infoObject;
            boxInformation.gameObject.SetActive(true); // Hiện thông tin
        }
    }

    void OnMouseExit()
    {
        // Ẩn thông tin khi chuột rời khỏi Box
        if (infoText != null)
        {
            boxInformation.gameObject.SetActive(false);
            nameText.text = "";
            infoText.text = "";
        }
    }
}
