using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPrefab; // Prefab thông báo
    public Transform notificationPanel; // Panel chứa thông báo
    public float notificationDuration = 5f; // Thời gian hiển thị mỗi thông báo

    public void ShowNotification(string message)
    // public void ShowNotification(Sprite icon, string message)
    {
        // Tạo thông báo mới từ prefab
        GameObject newNotification = Instantiate(notificationPrefab, notificationPanel);
        // Gán icon và message
        // newNotification.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        newNotification.transform.Find("Message").GetComponent<Text>().text = message;
        // // Bắt đầu đếm ngược để xóa thông báo
        // StartCoroutine(RemoveNotificationAfterTime(newNotification, notificationDuration));
    }

    // private IEnumerator RemoveNotificationAfterTime(GameObject notification, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(notification); // Xóa thông báo sau thời gian delay
    // }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            ShowNotification("Thông báo mới!");
        }
    }
}
