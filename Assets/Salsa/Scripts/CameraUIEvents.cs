using UnityEngine;

public class CameraUIEvents : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject creditUI;

    public void HideAllUI()
    {
        if (menuUI != null) menuUI.SetActive(false);
        if (creditUI != null) creditUI.SetActive(false);
    }

    public void ShowMenuUI()
    {
        if (menuUI != null) menuUI.SetActive(true);
    }

    public void ShowCreditUI()
    {
        if (creditUI != null) creditUI.SetActive(true);
    }
}
