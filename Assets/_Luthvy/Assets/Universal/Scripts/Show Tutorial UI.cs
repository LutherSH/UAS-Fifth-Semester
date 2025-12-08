using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UITorielCanvas;
    void Start()
    {
        UITorielCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UITorielCanvas.SetActive(true);
        }
        else
        {
            UITorielCanvas.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
         UITorielCanvas.SetActive(false);
    }
}
