using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningScript : AttackScript
{
    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] GameObject summoningCircle;
    private HealthSystem hs;
    private int maxHp;
    private bool phase1 = false;
    private bool phase2 = false;
    private bool phase3 = false;
    [SerializeField] private float offSet;

    protected override void Awake()
    {
        base.Awake();
        hs = GetComponentInParent<HealthSystem>();
        maxHp = hs.health;
    }

    protected override void Start()
    {
        StartCoroutine(CheckPhase());
    }

    IEnumerator CheckPhase()
    {
        while (!phase3)
        {
            if (!phase1)
            {
                phase1 = true;
            }
            if (hs.health < maxHp * 0.66f && !phase2)
            {
                phase2 = true;
            }
            if (hs.health < maxHp * 0.33f && !phase3)
            {
                phase3 = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Attack()
    {
        if (!onCooldown && InRange() && gm.gameIsOn)
        {
            Debug.Log(phase1);
            AttackAnimation();
            audioSource.PlayOneShot(attackSound);
            if (phase1)
                SummonMonster(0, 4);
            if (phase2)
                SummonMonster(1, 3);
            if (phase3)
                SummonMonster(2, 2);
            StartCoroutine(CooldownCoroutine());
        }
    }

    private void SummonMonster(int index, int number)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 vOffSet = new Vector2(Random.Range(-offSet, offSet), Random.Range(-offSet, offSet));
            Instantiate(monsterPrefabs[index], (Vector2)transform.position + vOffSet, monsterPrefabs[index].transform.rotation);
            GameObject summCircle = Instantiate(summoningCircle, (Vector2)transform.position + vOffSet, summoningCircle.transform.rotation);
            Destroy(summCircle, 0.5f);
        }
    }
}
