using UnityEngine;

public class UITrigger : MonoBehaviour
{
    [Header("UI Panel Tutorial")]
    public GameObject tutorialPanel;

    [Header("Opsi")]
    public bool autoHide = false;        // Apakah panel hilang otomatis?
    public float hideDelay = 3f;         // Waktu tunggu sebelum panel hilang
    public bool requireButton = false;   // Apakah harus tekan tombol untuk lanjut?
    public KeyCode continueKey = KeyCode.E; // Tombol untuk lanjut

    private bool isPlayerInside = false;
    private bool isShown = false;

    private void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && requireButton && Input.GetKeyDown(continueKey))
        {
            HideTutorial();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShown)
        {
            ShowTutorial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !requireButton)
        {
            HideTutorial();
        }
    }

    void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            isShown = true;
            isPlayerInside = true;

            if (autoHide)
                Invoke(nameof(HideTutorial), hideDelay);
        }
    }

    void HideTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        isPlayerInside = false;
        isShown = false;
    }
}