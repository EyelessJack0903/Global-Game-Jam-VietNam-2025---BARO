using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMaanager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // Thoát ứng dụng trong phiên bản build
#endif
    }
}
