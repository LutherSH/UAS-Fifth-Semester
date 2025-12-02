using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasMenu;
    public GameObject credit;
    public GameObject theEaserEgg;
    public GameObject theMainCam;
    void Awake()
    {
        canvasMenu.SetActive(true);
        credit.SetActive(false);
        theEaserEgg.SetActive(true);
        theMainCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
