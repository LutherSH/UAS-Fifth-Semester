using UnityEngine;

public class DeadStateMelee : TheStateMelee
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyMelee enemy;
    //private float idleTimer;
    //private float idleDuration;

    public DeadStateMelee(EnemyMelee enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        Debug.Log("Dead State");
        enemy.nAgent.isStopped = true;
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