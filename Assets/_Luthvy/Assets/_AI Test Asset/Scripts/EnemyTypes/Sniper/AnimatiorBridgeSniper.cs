using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorBridgeSniper : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemySniper enemy;

    public void NotAllowShoot()
    {
        enemy.allowShoot = false;
    }

    public void AllowShoot()
    {
        enemy.allowShoot = true;
    }

    public void DeathDespawn()
    {
        enemy.Despawn();
    }
}

