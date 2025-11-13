using UnityEngine;

public class ChaseStateMelee : TheStateMelee
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyMelee enemy;
    //private Transform player;

    public ChaseStateMelee(EnemyMelee enemyAI) //Transform playerTransform) // REGISTER STATE AND THE PROPERTIES
    {
        enemy = enemyAI;
        //player = playerTransform;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        //Debug.Log("Entering Chase");

        enemy.eMAnimator.SetBool("m_walk",true);
        if (enemy.nAgent != null)
        {
            enemy.nAgent.isStopped = false;
            //enemy.nAgent.updateRotation = false;
            enemy.fov = 300f;
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE
public void Update()
{
    
    if (enemy.player == null)
            return;

    // Keep chasing
    enemy.nAgent.SetDestination(enemy.player.position);

    // Face the player
    Vector3 direction = (enemy.player.position - enemy.transform.position).normalized;
    direction.y = 0; // keep upright, no tilting

    if (direction.magnitude > 0.01f)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Slerp(
            enemy.transform.rotation,
            lookRotation,
            Time.deltaTime * 5f // turn speed
        );
    }

        // State switches
    
    if (enemy.playerInAttackRange == true)
    {
        //Debug.LogWarning("ATTACk");
        enemy.SwitchState(new AttackStateMelee(enemy));
    }

    else if (!enemy.playerInSightRange)
        {
            enemy.SwitchState(new IdleStateMelee(enemy));
        }
}

    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {
        //Debug.Log("Exiting Chase");
        //enemy.nAgent.updateRotation = true;  
        enemy.eMAnimator.SetBool("m_walk",false);
    }
}