using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////
    /// UI PROPERTIES
    [Header("UI")]
    public TextMeshProUGUI objectiveTextUI;
    public GameObject objectivePanel; // Optional UI background panel

    private BaseEnemyEvent currentEvent;

    /////////////////////////////////////////////////////////////////////
    /// START
    private void Start()
    {
        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }

    /////////////////////////////////////////////////////////////////////
    /// CALLED BY B.E.E Script
    public void SetActiveObjective(BaseEnemyEvent newEvent)
    {
        if (newEvent == null) return;

        currentEvent = newEvent;
        ShowObjectiveText(newEvent.objectiveDescription);
    }

    public void ClearObjective(BaseEnemyEvent oldEvent)
    {
        if (currentEvent == oldEvent)
        {
            HideObjectiveText();
            currentEvent = null;
        }
    }

    public void OnObjectiveCompleted(string objectiveID)
    {
        if (currentEvent != null && currentEvent.objectiveID == objectiveID)
        {
            ShowObjectiveText($"Objective Complete: {currentEvent.objectiveDescription}");
            Invoke(nameof(HideObjectiveText), 2.5f); // Hide after 2.5 seconds
        }
    }
    /////////////////////////////////////////////////////////////////////
    /// OM PRIVATE PARTS
    private void ShowObjectiveText(string text)
    {
        if (objectiveTextUI != null)
            objectiveTextUI.text = text;

        if (objectivePanel != null)
            objectivePanel.SetActive(true);
    }

    private void HideObjectiveText()
    {
        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }
}
