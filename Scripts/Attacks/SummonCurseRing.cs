using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCurseRing : AttackScript
{
    [SerializeField] GameObject[] curseRingPrefabs;
    public override void Attack()
    {
        if(!onCooldown && InRange() && gm.gameIsOn)
        {
            AttackAnimation();
            StartCoroutine(CooldownCoroutine());
            SummonRing();
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void SummonRing()
    {
        int index = Random.Range(0, curseRingPrefabs.Length);
        GameObject instance = Instantiate(curseRingPrefabs[index], target.transform.position, curseRingPrefabs[index].transform.rotation);
        instance.GetComponent<CurseRing>().SetTarget(target);
    }
}
