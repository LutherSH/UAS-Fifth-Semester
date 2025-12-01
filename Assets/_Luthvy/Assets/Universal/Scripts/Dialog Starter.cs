using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogStarter : MonoBehaviour
{
    public Flowchart flowchart;
    
    public string blockName;
    public bool isPlayed = false;
    public bool isTrigsOnce = false;
    public bool triggerUnityEvent;
    // Start is called before the first frame update
    [SerializeField] private UnityEvent unityEvent;
    void Start()
    {
        isPlayed = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && flowchart != null && !triggerUnityEvent && !isPlayed)
        {
            ExecuteBlock();
            //isPlayed = true;
        }

        if (unityEvent !=null && triggerUnityEvent && !isPlayed)
        {
            unityEvent.Invoke();
        }

        if (isTrigsOnce)
        {
            isPlayed = true;
        }
    }

    public void ExecuteBlock()
    {
        flowchart.ExecuteBlock(blockName);
    }

    //public void AddCallerUE
}
