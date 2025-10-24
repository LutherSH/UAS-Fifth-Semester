using UnityEditor;
using UnityEngine;

public class DeadStateBow : TheStateBow
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyBow enemy;
    //private float idleTimer;
    //private float idleDuration;

    public DeadStateBow(EnemyBow enemyAI) // REGISTER STATE
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