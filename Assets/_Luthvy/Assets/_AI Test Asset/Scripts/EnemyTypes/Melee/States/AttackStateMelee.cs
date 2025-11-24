using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMelee : TheStateMelee
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyMelee enemy;
    private Transform player;
    private bool isAttacking;
    public AttackStateMelee(EnemyMelee enemyAI)//, Transform playerTrasform) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        //Debug.Log("ATTAAAACCCCKKK");
        //enemy.nAgent.isStopped = true;

        enemy.eMAnimator.SetTrigger("m_attack");
        enemy.fov = 359f;
        isAttacking = false;

        //StartAttackWindow(); // <---------
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE

    public void Update()
    {
        enemy.triggerHitboxAttacking = isAttacking;
        // Face the player
        Vector3 dir = (enemy.player.position - enemy.transform.position);
        dir.y = 0;
        if (dir.magnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRot, Time.deltaTime * 6f);
        }

        // Attack cooldown control
        if (!isAttacking && Time.time >= enemy.nextAttackTime)
        {
            //enemy.animator.SetTrigger("Attack");

            enemy.nextAttackTime = Time.time + enemy.attackCooldown;
            isAttacking = true;
        }

        // Return to chase if out of range
        if (!enemy.playerInAttackRange)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new ChaseStateMelee(enemy));
            return;
        }
    }

    public void PlayShootSound()
    {
        if (enemy.audioSource != null && enemy.shootClip != null)
        {
            enemy.audioSource.PlayOneShot(enemy.shootClip);
        }
    }


    ///////////////////////////////////////////////////////////////////////
    /// STATE EXIT
    public void Exit()
    {
        Debug.Log("Exiting Attack");
        if (enemy.weaponCollider != null)
            enemy.weaponCollider.enabled = false;
    }
}
