using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : Damageable
{
    public float BackUpTime = 2f;
    //Animator ani;

    float hurt = 0f;
    float previousHealth;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        previousHealth = health;
        AddCallOnDamage(HurtTarget);
        AddCallOnDeath(TargetDied);
    }

    public override void Update()
    {
        hurt = Mathf.Lerp(hurt, (isDead()) ? 1f : 0f, Time.deltaTime * 4f);
    }

    void HurtTarget()
    {
        if (isDead()) return;
        float dmgDone = (previousHealth - health);
        hurt = dmgDone / (maxHealth * 2);
    }

    void TargetDied()
    {
        // StartCoroutine(getBackUp());
        // IEnumerator getBackUp()
        // {
        //     hurt = 1f;
        //     yield return new WaitForSeconds(BackUpTime);
        //     Heal();
        // }
    }
}
