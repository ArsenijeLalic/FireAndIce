using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackScript
{
    private HealthSystem targetHs;
    public override void Attack()
    {
        if (!onCooldown && InRange() && gm.gameIsOn)
        {
            AttackAnimation();
            StartCoroutine(CooldownCoroutine());
            Damage();
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void Damage()
    {
        if (targetHs == null)
        {
            targetHs = target.GetComponent<HealthSystem>();
        }
        targetHs.UpdateHealth(-damage, "physical");
    }
}
