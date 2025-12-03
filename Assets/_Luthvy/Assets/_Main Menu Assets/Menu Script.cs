using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private GameObject lightMe;
    private GameObject rawIMG;
    //private bool itsOn = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rawIMG = GameObject.Find("plane3");
        lightMe = GameObject.Find("Point Light22");
    }

    void Start()
    {
        lightMe.SetActive(false);
        rawIMG.SetActive(false);
    }
    public void GoToThe()
    {
        animator.SetTrigger("GoToThe");
    }

    public void OnLight()
    {
        lightMe.SetActive(true);
        rawIMG.SetActive(true);
    }
    public void OffLight()
    {
        lightMe.SetActive(false);
        rawIMG.SetActive(false);
    }

}
