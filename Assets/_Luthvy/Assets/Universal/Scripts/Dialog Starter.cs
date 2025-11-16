using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class DialogStarter : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName;
    public bool isPlayed = false;
    // Start is called before the first frame update

    void Start()
    {
        isPlayed = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPlayed)
        {
            flowchart.ExecuteBlock(blockName);
            isPlayed = true;
        }
    }
}
