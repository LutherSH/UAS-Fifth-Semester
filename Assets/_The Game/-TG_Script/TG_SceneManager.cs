using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerTG : MonoBehaviour
{
    public string sceneName;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject playerTrue;
    private float delayGameOver = 0.03f;

    //////////////////////////////////////////////////////////

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        playerTrue = GameObject.Find("PlayerTrue");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    //////////////////////////////////////////////////////////
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TemporaryGameWin()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Gameplay()
    {
        SceneManager.LoadScene("AI Test 1");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("G_MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;   
    }
    public void ShowGameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //gameOverScreen.SetActive(true);
        playerTrue.SetActive(false);
        EnsureEventSystem();
        StartCoroutine(DelayDeath());
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        //Application.Quit();
        Debug.LogWarning("QUIT GAME");
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(delayGameOver);
        gameOverScreen.SetActive(true);
        playerTrue.SetActive(true);
        Time.timeScale = 0f;
    }

    void EnsureEventSystem()
    {
        if (EventSystem.current == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            Debug.LogWarning("ensure");
        }
    }
}
