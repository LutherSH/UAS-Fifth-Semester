using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerTG : MonoBehaviour
{
    //public string sceneName;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject playerTrue;
    private float delayGameOver = 0.03f;
    private int sceneNummer;
    [HideInInspector] public bool playerIsDead = false;
    public InteractionController interactionController;
    //////////////////////////////////////////////////////////

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        playerTrue = GameObject.Find("PlayerTrue");
        RestartIC();
    }

    void Awake()
    {
        RestartIC();
        playerIsDead = false;
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
        //SceneManager.LoadScene(sceneName);
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
        SceneManager.LoadScene(1);
        sceneNummer = 1;
    }

    public void NextStage()
    {
        SceneManager.LoadScene(sceneNummer);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("G_MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;   
    }
    public void ShowGameOver()
    {
        playerIsDead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //gameOverScreen.SetActive(true);
        playerTrue.SetActive(false);
        EnsureEventSystem();
        StartCoroutine(DelayDeath());
    }

    public void RetryGame()
    {
        interactionController.enabled = false;
        //interactionController.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void RestartIC()
    {
        StartCoroutine(ResetIC());
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
        //InteractionController
        Time.timeScale = 0f;
    }

    IEnumerator ResetIC()
    {
        //playerTrue.SetActive(true);
        interactionController.enabled = false;
        Debug.LogWarning("Ic off");
        yield return new WaitForSeconds(1);
        Debug.LogWarning("Ic on");
        interactionController.enabled = true;
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
