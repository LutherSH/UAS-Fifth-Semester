using UnityEngine;

public class IdleStateGun : TheStateGun
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyGun enemy;
    private float idleTimer;
    private float idleDuration;

    public IdleStateGun(EnemyGun enemyAI) // REGISTER STATE
    {
        enemy = enemyAI;
        idleDuration = enemy.idleDuration;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        Debug.Log("Entering Idle");
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
            enemy.SwitchState(new ChaseStateGun(enemy));
            return;
        }

        else if (enemy.playerInSightRange && enemy.playerInAttackRange)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new AttackStateGun(enemy));
            return;
        }

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            if (enemy.nAgent != null)
            {
                enemy.nAgent.isStopped = false;
            }

            enemy.SwitchState(new PatrolStateGun(enemy));
        }
        if (enemy.isSpooked)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new PatrolStateGun(enemy));
            return;    
        }
        //if (enemy.isSpooked && !enemy.playerInSightRange) enemy.SwitchState(new PatrolStateGun(enemy));
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {
        Debug.Log("Exiting Idle");
        if (enemy.nAgent != null)
        {
            enemy.nAgent.isStopped = false;
        }
    }
}