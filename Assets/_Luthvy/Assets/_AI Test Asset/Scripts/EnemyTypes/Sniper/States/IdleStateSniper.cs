using System.Collections;
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
        enemy.eSAnimator.SetBool("s_idle",true);
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
            Vector3 shootDir = (enemy.player.position + Vector3.up * 0.5f - enemy.firePoint.position).normalized;

            Ray ray = new Ray(enemy.firePoint.position, shootDir);
            RaycastHit hit;
            Vector3 hitPoint;

            if (Physics.Raycast(ray, out hit, enemy.attackRange, enemy.thePlayer | enemy.theWall))
            {
                hitPoint = hit.point;

                if (enemy.bulletTrail != null) enemy.StartCoroutine(SpawnTrail(hitPoint));
                //Debug.Log("Gun hit: " + hit.collider.name);

                // if (hit.collider.CompareTag("Player"))
                // {
                //     //Debug.LogWarning("Hit player " + enemy.attackDamage);
                //     //enemy.playerBehaviour.PlayerTakeDmg(enemy.attackDamage);
                // }

                Debug.DrawRay(enemy.firePoint.position, shootDir * enemy.sightRange, Color.green, 0.5f);
                //}
                idleTimer += Time.deltaTime;

                Debug.LogWarning(idleTimer);

                if (idleTimer >= reactionDelay)
                {
                    enemy.nAgent.isStopped = false;
                    enemy.SwitchState(new AttackStateSniper(enemy));
                    return;
                }
            }

            //if (enemy.sightLazer != null)
            //enemy.StartCoroutine(SpawnTrail(hitPoint));

            //else
            //{
            //hitPoint
            //}
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
    }

    private IEnumerator SpawnTrail(Vector3 hitPoint)
    {
        // Create trail object
        GameObject trail = GameObject.Instantiate(enemy.sightLazer, enemy.firePoint.position, Quaternion.identity);
        TrailRenderer tr = trail.GetComponent<TrailRenderer>();

        // Just in case the prefab doesn’t auto-play
        tr.Clear();

        Vector3 start = enemy.firePoint.position;
        Vector3 end = hitPoint;
        float distance = Vector3.Distance(start, end);
        float speed = enemy.bulletSpeed; 
        float time = 0f;

        while (time < distance / speed)
        {
            time += Time.deltaTime;
            trail.transform.position = Vector3.Lerp(start, end, time * speed / distance);
            yield return null;
        }

        trail.transform.position = end;
        GameObject.Destroy(trail, tr.time);
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

        enemy.eSAnimator.SetBool("s_idle",false);
    }
}