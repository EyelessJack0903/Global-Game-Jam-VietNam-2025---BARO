using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WallClock : MonoBehaviour
{
    public RectTransform minuteHand; // Kim phút
    public RectTransform hourHand;   // Kim giờ

    public float duration = 180f;    // Thời gian để quay từ 6h đến 7h (3 phút = 180 giây)
    private float elapsedTime = 0f;  // Thời gian đã trôi qua
    private float timeScale = 1f;    // Tốc độ thời gian (1x là bình thường)

    private const float hoursToDegrees = 360 / 12;  // Đổi giờ sang góc (1 giờ = 30°)
    private const float minutesToDegrees = 360 / 60; // Đổi phút sang góc (1 phút = 6°)

    public float startHour = 6f; // Giờ bắt đầu
    public float endHour = 7f;   // Giờ kết thúc
    [SerializeField] private MiniGameController miniGameController;


    void Update()
    {
        // Tăng thời gian đã trôi qua với tốc độ được điều chỉnh bởi timeScale
        elapsedTime += Time.deltaTime * timeScale;

        // Tính tỷ lệ thời gian đã trôi qua (0 đến 1)
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Tính giờ hiện tại (nội suy giữa 6h và 7h)
        float currentHour = Mathf.Lerp(startHour, endHour, t);

        // Tính phút hiện tại dựa trên phần thập phân của giờ (ví dụ: 6.5h = 30 phút)
        float currentMinute = (currentHour - Mathf.Floor(currentHour)) * 60;

        // Xoay kim giờ
        if (hourHand != null)
        {
            hourHand.rotation = Quaternion.Euler(0, 0, -currentHour * hoursToDegrees);
        }

        // Xoay kim phút
        if (minuteHand != null)
        {
            minuteHand.rotation = Quaternion.Euler(0, 0, -currentMinute * minutesToDegrees);
        }

        // Log cảnh báo khi đạt 7h
        if (Mathf.Approximately(currentHour, 7f))
        {
            Debug.Log("Cảnh báo: Đồng hồ đã đạt 7 giờ!");
            SceneManager.LoadScene("Company");
        }

        if (Mathf.Approximately(currentHour, 12f))
        {
            miniGameController.canPlayDialogueMinigame = true;
        }
    }

    // Hàm để tăng tốc thời gian
    public void SetTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
        Debug.Log($"Tốc độ thời gian đã được đặt thành: {timeScale}x");
    }
}
