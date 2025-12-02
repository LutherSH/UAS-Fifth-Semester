using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateGun : TheStateGun
{
///////////////////////////////////////////////////////////////////////
/// PROPERTIES OF STATE
    private EnemyGun enemy;
    private Transform player;
    public AttackStateGun(EnemyGun enemyAI)//, Transform playerTrasform) // REGISTER STATE
    {
        enemy = enemyAI;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE ENTER
    public void Enter()
    {
        //Debug.Log("ATTAAAACCCCKKK");
        enemy.eGAnimation.SetTrigger("g_attack");

        enemy.nAgent.isStopped = true;
        enemy.fov = 359f;
    }

    ///////////////////////////////////////////////////////////////////////
    /// STATE UPDATE

public void Update()
{
    // Rotate to face player
    Vector3 lookDir = (enemy.player.position - enemy.transform.position).normalized;
    lookDir.y = 0f;
    enemy.transform.rotation = Quaternion.LookRotation(lookDir);

    // Fire if cooldown is ready
    if (enemy.playerInAttackRange && Time.time >= enemy.nextFireTime && enemy.playerInSightRange && enemy.allowShoot)
    {
        FireGun();
        enemy.audioSource.PlayOneShot(enemy.shootClip);
        enemy.nextFireTime = Time.time + enemy.fireCooldown;
    }

    // Transition back to chase if player moves away
    if (!enemy.playerInAttackRange)
    {
        enemy.nAgent.isStopped = false;
        enemy.SwitchState(new ChaseStateGun(enemy));
        return;
    }
}

private void FireGun()
{
    // Base direction
    Vector3 shootDir = (enemy.player.position + Vector3.up * 0.5f - enemy.firePoint.position).normalized;

    // Add random spread (in degrees)
    float spread = enemy.bulletInaccuracy;
        shootDir = Quaternion.Euler(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0
        ) * shootDir;

    // Now fire the ray
    if (enemy.bulletInaccuracy >= enemy.maxAccuracy) { enemy.bulletInaccuracy = enemy.bulletInaccuracy - 0.5f; }
    
    Ray ray = new Ray(enemy.firePoint.position, shootDir);
    RaycastHit hit;
    Vector3 hitPoint;

    if (Physics.Raycast(ray, out hit, enemy.attackRange, enemy.thePlayer | enemy.theWall))
    {
        hitPoint = hit.point;
        Debug.Log("Gun hit: " + hit.collider.name);

        if (hit.collider.CompareTag("Player"))
            {
            //Debug.LogWarning("Hit player " + enemy.attackDamage);
            enemy.playerBehaviour.PlayerTakeDmg(enemy.attackDamage);
        }

        if (enemy.muzzleFlash != null)
            GameObject.Instantiate(enemy.muzzleFlash, enemy.firePoint.position, enemy.firePoint.rotation);

        if (enemy.hitEffect != null)
            GameObject.Instantiate(enemy.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
    else
    {
        hitPoint = enemy.firePoint.position + shootDir * enemy.attackRange;
    }

    if (enemy.bulletTrail != null)
        enemy.StartCoroutine(SpawnTrail(hitPoint));

    Debug.DrawRay(enemy.firePoint.position, shootDir * enemy.attackRange, Color.yellow, 0.3f);
}


private IEnumerator SpawnTrail(Vector3 hitPoint)
{
    // Create trail object
    GameObject trail = GameObject.Instantiate(enemy.bulletTrail, enemy.firePoint.position, Quaternion.identity);
    TrailRenderer tr = trail.GetComponent<TrailRenderer>();

    // Just in case the prefab doesn’t auto-play
    tr.Clear();

    Vector3 start = enemy.firePoint.position;
    Vector3 end = hitPoint;
    float distance = Vector3.Distance(start, end);
    float speed = enemy.bulletSpeed; // define this in your EnemyGun script (e.g. 200f)
    float time = 0f;

    while (time < distance / speed)
    {
        time += Time.deltaTime;
        trail.transform.position = Vector3.Lerp(start, end, time * speed / distance);
        yield return null;
    }

    trail.transform.position = end;
    GameObject.Destroy(trail, tr.time); // let it fade naturally
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

    }
}
