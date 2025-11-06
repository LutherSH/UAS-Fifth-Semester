using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningManager : MonoBehaviour
{
    public GameObject winScreen;

    void Start()
    {
        if (winScreen != null)
            winScreen.SetActive(false);

        LockCursor();
    }

    public void ShowWin()
    {
        if (winScreen != null)
            winScreen.SetActive(true);

        Time.timeScale = 0f;

        UnlockCursor();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        LockCursor();
    }

    public void BackToMenu(string menuScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);

        LockCursor();
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
