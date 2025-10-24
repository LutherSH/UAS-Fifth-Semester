using UnityEngine;

public class DeadStateGun : TheStateGun
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyGun enemy;
    //private float idleTimer;
    //private float idleDuration;

    public DeadStateGun(EnemyGun enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        Debug.Log("Dead State");
        enemy.nAgent.isStopped = true;
        enemy.Despawn();
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE
    public void Update()
    {
        return;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {

    }
}