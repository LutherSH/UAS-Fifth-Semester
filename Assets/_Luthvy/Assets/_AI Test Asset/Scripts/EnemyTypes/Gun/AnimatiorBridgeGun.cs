using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorBridgeGun : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyGun enemy;

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

