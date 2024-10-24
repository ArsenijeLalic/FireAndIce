using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGunScript : ShootProjectile
{
    [SerializeField] private float fireRate;
    [SerializeField] private int numOfProjectiles;

    public override void Attack()
    {
        if (!onCooldown)
        {
            AttackAnimation();
            StartCoroutine(MiniGunCoroutine());
            StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator MiniGunCoroutine()
    {
        for(int i = 0; i < numOfProjectiles; i++)
        {
            Shoot(takeAim());
            yield return new WaitForSeconds(fireRate);
        }
    }
}
