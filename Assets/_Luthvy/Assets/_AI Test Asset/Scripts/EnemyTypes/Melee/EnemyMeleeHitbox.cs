using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeHitbox : MonoBehaviour
{
    public EnemyMelee enemy;
    public PlayerBehaviour playerBehaviour;

    private GameObject thePlayer;

    void Awake()
    {
        thePlayer = GameObject.Find("PlayerTrue");
        playerBehaviour = thePlayer.GetComponentInChildren<PlayerBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Melle hit");
            playerBehaviour.PlayerTakeDmg(enemy.attackDamage);

        }
    }
}
