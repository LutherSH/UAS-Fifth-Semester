using UnityEngine;

public class DeadStateSniper : TheStateSniper
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemySniper enemy;
    //private float idleTimer;
    //private float idleDuration;

    public DeadStateSniper(EnemySniper enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        Debug.Log("Dead State");
        enemy.nAgent.isStopped = true;
        //enemy.Despawn();
        enemy.eSAnimator.SetTrigger("s_dies");
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