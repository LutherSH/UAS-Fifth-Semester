using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorBridgeMelee : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyMelee enemy;

    public void AllowAttackOpening()
    {
        enemy.StartAttackWindow();
    }

    public void NotAllowAttackOpening()
    {
        enemy.EndAttackWindow();
    }

    public void DeathDespawn()
    {
        enemy.Despawn();
    }
}

