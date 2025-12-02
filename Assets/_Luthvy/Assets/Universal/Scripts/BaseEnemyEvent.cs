using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BaseEnemyEvent : MonoBehaviour
{
/////////////////////////////////////////////////////////////////////
/// GENERAL SETTINGS
    [Header("Settings")]
    public string enemyTag = "Enemy";
    public bool triggerOnce = true;
    public bool lastEvent;
    public bool dialogTrigger = false;
    private GameObject theEventGameobject;
    public GameObject NoGoingBackBarrier;

/////////////////////////////////////////////////////////////////////
/// OBJECTIVE INFO (For ObjectiveManager)
    [Header("Objective Info")]
    public string objectiveID;
    [TextArea] 
    public string objectiveDescription;
    public ObjectiveManager objectiveManager; 

/////////////////////////////////////////////////////////////////////
/// EVENT OBJECTS
    [Header("Rewards / Linked Objects")]
    public GameObject barrierObject;
    public GameObject activateNextEvent;
    //public int indexSceneNext;

/////////////////////////////////////////////////////////////////////
/// UNITY EVENTS
    [Header("Event Callbacks")]
    [Tooltip("Triggered only if 'lastEvent' is checked.")]
    [SerializeField] private UnityEvent onLastEventTriggered;
    [SerializeField] private UnityEvent forDialogOnly;

/////////////////////////////////////////////////////////////////////
/// INTERNAL STATE
    public List<GameObject> enemiesInArea = new List<GameObject>();
    private bool triggered = false;

/////////////////////////////////////////////////////////////////////
/// INITIALIZATION
    private void Start()
    {
        theEventGameobject = gameObject;

        // Safe checks to prevent null errors
        if (barrierObject != null)
            barrierObject.SetActive(true);

        if (activateNextEvent != null)
            activateNextEvent.SetActive(false);


        // FIND ENEMIES INSIDE AREA
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(enemyTag))
            {
                enemiesInArea.Add(hit.gameObject);
            }
        }

        Debug.Log($"[{name}] Found {enemiesInArea.Count} enemies in area.");

        if (NoGoingBackBarrier !=null)
        {
            NoGoingBackBarrier.SetActive(false);
        }
    }

/////////////////////////////////////////////////////////////////////
/// MAIN UPDATE LOOP
    private void Update()
    {
        if (triggered && triggerOnce) return;

        // Clean up null/destroyed enemies from the list
        enemiesInArea.RemoveAll(e => e == null || !e.activeInHierarchy);

        // Check all enemies
        if (enemiesInArea.Count == 0)
        {
            triggered = true;
            OnAllEnemiesDead();
        }

        // If not last event → activate the next
        if (!lastEvent && triggered)
        {
            if (activateNextEvent != null)
                activateNextEvent.SetActive(true);
        }

        // If last event → trigger UnityEvent
        if (lastEvent && triggered)
        {
            //Debug.Log($"[{name}] Final event completed triggering UnityEvent");
            onLastEventTriggered?.Invoke();
        }
    }

/////////////////////////////////////////////////////////////////////
/// EVENT COMPLETION HANDLER
    private void OnAllEnemiesDead()
    {
        Debug.LogWarning($"All enemies inside {name} area are gone! Objective '{objectiveID}' complete.");

        if (barrierObject != null)
            barrierObject.SetActive(false);

        // Notify Objective Manager
        if (objectiveManager != null)
        {
            objectiveManager.OnObjectiveCompleted(objectiveID);
        }

        if (dialogTrigger)
        {
            forDialogOnly?.Invoke();
        }
        
        // Deactivate this event area
        theEventGameobject.SetActive(false);
    }
    
/////////////////////////////////////////////////////////////////////
/// PLAYER DETECTION    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectiveManager != null)
        {
            objectiveManager.SetActiveObjective(this);
            //SpookAllEnemies();

            if (NoGoingBackBarrier !=null)
            {
                NoGoingBackBarrier.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && objectiveManager != null)
        {
            objectiveManager.ClearObjective(this);
        }
    }

    public void SpookAllEnemies()
    {
        foreach (GameObject enemyObj in enemiesInArea)
        {
            if (enemyObj == null) continue;

            ISpookable spookable = enemyObj.GetComponent<ISpookable>();
            if (spookable != null)
            {
                spookable.Spooked();
            }
        }

        Debug.LogWarning($"[{name}] All enemies inside event area have been SPOOKED.");
    }

/////////////////////////////////////////////////////////////////////
/// DEBUG VISUALIZATION
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
