using UnityEngine;

public class IdleStateMelee : TheStateMelee
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyMelee enemy;
    private float idleTimer;
    private float idleDuration;

    public IdleStateMelee(EnemyMelee enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
        idleDuration = enemy.idleDuration;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        //Debug.Log("Entering Idle");
        idleTimer = 0f;
        if (enemy.nAgent != null)
        {
            enemy.nAgent.isStopped = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE
    public void Update()
    {
        if (enemy.playerInSightRange && !enemy.playerInAttackRange)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new ChaseStateMelee(enemy));
            return;
        }

        else if (enemy.playerInSightRange && enemy.playerInAttackRange)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new AttackStateMelee(enemy));
            return;
        }

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            if (enemy.nAgent != null)
            {
                enemy.nAgent.isStopped = false;
            }

            enemy.SwitchState(new PatrolStateMelee(enemy));
        }

        if (enemy.isSpooked)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new PatrolStateMelee(enemy));
            return;    
        }

        //if (enemy.isSpooked && !enemy.playerInSightRange) enemy.SwitchState(new PatrolStateMelee(enemy));
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {
        //Debug.Log("Exiting Idle");
        if (enemy.nAgent != null)
        {
            enemy.nAgent.isStopped = false;
        }
    }
}