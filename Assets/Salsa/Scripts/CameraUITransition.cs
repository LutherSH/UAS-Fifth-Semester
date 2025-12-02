using UnityEngine;

public class CameraUITransition : MonoBehaviour
{
    public Animator cameraAnimator;
    public CameraUIEvents events;

    public void GoToCredit()
    {
        events.HideAllUI();
        cameraAnimator.SetTrigger("GoCredit");
    }

    public void GoToMenu()
    {
        events.HideAllUI();
        cameraAnimator.SetTrigger("GoMenu");
    }
}
