using UnityEngine;

public class IdleStateSniper : TheStateSniper
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemySniper enemy;
    private float idleTimer;
    private float reactionDelay;

    public IdleStateSniper(EnemySniper enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
        reactionDelay = enemy.reactionDelay;
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
        // if (enemy.playerInSightRange && !enemy.playerInAttackRange)
        // {
        //     enemy.nAgent.isStopped = false;
        //     enemy.SwitchState(new ChaseStateGun(enemy));
        //     return;
        // }
        
        if (enemy.playerInSightRange && enemy.playerInAttackRange)
        {
            idleTimer += Time.deltaTime;

            Debug.LogWarning(idleTimer);

            if (idleTimer >= reactionDelay)
            {
                enemy.nAgent.isStopped = false;
                enemy.SwitchState(new AttackStateSniper(enemy));
                return;
            }

        }

        // if (idleTimer >= idleDuration)
        // {
        //     if (enemy.nAgent != null)
        //     {
        //         enemy.nAgent.isStopped = false;
        //     }

        //     enemy.SwitchState(new PatrolStateGun(enemy));
        // }
        // if (enemy.isSpooked)
        // {
        //     enemy.nAgent.isStopped = false;
        //     enemy.SwitchState(new PatrolStateGun(enemy));
        //     return;    
        // }
        //if (enemy.isSpooked && !enemy.playerInSightRange) enemy.SwitchState(new PatrolStateGun(enemy));
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {
        //Debug.Log("Exiting Idle");
        if (enemy.nAgent != null)
        {
            enemy.nAgent.isStopped = false;
            idleTimer = 0f;
        }
    }
}