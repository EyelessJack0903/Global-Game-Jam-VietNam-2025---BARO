using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("Tên của Scene cần tải khi nhấn nút")]
    public string sceneName;

    [Tooltip("Canvas hiển thị Credits")]
    public Canvas credits;

    // Chuyển cảnh
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Tên scene chưa được gán. Hãy đảm bảo tên scene được thiết lập trong Inspector.");
            return;
        }

        // Kiểm tra scene có tồn tại trong Build Settings không
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' không tồn tại trong Build Settings. Kiểm tra lại tên scene.");
        }
    }

    // Thoát game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Mở canvas Credits
    public void OpenCredits()
    {
        if (credits != null)
        {
            credits.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas Credits chưa được gán trong Inspector.");
        }
    }

    // Đóng canvas Credits
    public void CloseCredits()
    {
        if (credits != null)
        {
            credits.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas Credits chưa được gán trong Inspector.");
        }
    }
}
