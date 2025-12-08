using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NoSwimInsstantDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerBehaviour playerBehaviour;
    public GameObject PlayerOb;
    void Awake()
    {
        PlayerOb = GameObject.Find("PlayerTrue");
        playerBehaviour = PlayerOb.GetComponentInChildren<PlayerBehaviour>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerBehaviour.PlayerTakeDmg(1000);
        }
    }
}
