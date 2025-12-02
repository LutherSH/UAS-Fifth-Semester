using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class AttackStateBow : TheStateBow
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyBow enemy;
    private Transform player;

    public AttackStateBow(EnemyBow enemyAI)//, Transform playerTrasform) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        //Debug.Log("ATTAAAACCCCKKK");
        //enemy.maxArrowSpeed = enemy.arrowSpeed * 1.5f;
        enemy.eAanimation.SetTrigger("b_attack");
        enemy.nAgent.isStopped = true;
        enemy.fov = 359f;
        //enemy.StartCoroutine(enemy.FirstDelay());
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE

    public void Update()
    {
        // Face the player
        Vector3 faceDir = (enemy.player.position - enemy.transform.position).normalized;
        faceDir.y = 0; // keep upright, no tilting


        if (faceDir.magnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(faceDir);
            enemy.transform.rotation = Quaternion.Slerp(
                enemy.transform.rotation,
                lookRotation,
                Time.deltaTime * 5f // turn speed
            );
        }

        if (enemy.playerInAttackRange && Time.time >= enemy.nextFireTime && enemy.playerInSightRange && enemy.allowShoot)
        {
            FireBow();
            enemy.audioSource.PlayOneShot(enemy.shootClip);
            enemy.nextFireTime = Time.time + enemy.fireCooldown;
        }

        if (!enemy.playerInAttackRange)
        {
            enemy.nAgent.isStopped = false;
            enemy.SwitchState(new ChaseStateBow(enemy));
            return;
        }
    }
    
    ///////////////////////////////////////////////////////////////////////
    /// STATE FIREEEEE
    public void FireBow()
    {
        // Face the player
        Vector3 shootDir = (enemy.player.position - enemy.transform.position).normalized;
        shootDir.y = 0; // keep upright, no tilting
        // Add random spread (in degrees)
        float spread = enemy.bulletInaccuracy;
        Vector3 InacureDir = Quaternion.Euler(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0
        ) * shootDir;

        if (enemy.bulletInaccuracy >= enemy.maxAccuracy) { enemy.bulletInaccuracy = enemy.bulletInaccuracy - 0.5f; }
        if (enemy.arrowSpeed <= enemy.maxArrowSpeed) { enemy.arrowSpeed = enemy.arrowSpeed + 4f; }

        //Debug.LogWarning(enemy.maxArrowSpeed + "/" + enemy.arrowSpeed);
        
        // Spawn position
        Vector3 spawnPos = enemy.firePoint != null ? enemy.firePoint.position : enemy.transform.position + enemy.transform.forward * 1.0f;

        // Direction to player
        Vector3 dir = (enemy.player.position - spawnPos);
        if (dir.sqrMagnitude < 0.0001f)
        {
            dir = enemy.firePoint != null ? enemy.firePoint.forward : enemy.transform.forward;
        }
        dir.Normalize();

        // Rotation that looks along 'dir'
        Quaternion rot = Quaternion.LookRotation(dir);

        // Bullet model's "nose" isn't on +Z, tweak here:
        rot *= Quaternion.Euler(180f, 0f, 0f); // <- tweak prefab

        // Instantiate with the computed rotation
        GameObject bullet = Object.Instantiate(enemy.arrowPrevab, spawnPos, rot);

        // Give it velocity if it has a Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = InacureDir * enemy.arrowSpeed;
        }
        else
        {
            // fallback for non-Rigidbody bullets: orient then move in Update()
            bullet.transform.forward = dir;
        }

    //Debug.Log("Fired bullet at player (dir: " + dir + ")");
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
        //Debug.Log("Exiting Attack");
        enemy.eAanimation.SetTrigger("b_attack");
    }
}
