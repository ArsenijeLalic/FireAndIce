using UnityEngine;
using Pathfinding;
using System.Collections;

public class Enemy : Unit
{
    public AIPath aiPath;
    private GameObject player;
    [SerializeField] private float aggroRange;
    private bool aggroed = false;
    override protected void Awake()
    {
        Init();
        defaultSpeed = aiPath.maxSpeed;
        currSpeed = 0;
        player = GameObject.Find("Player");
        
        StartCoroutine(CheckPlayerProximity());
        foreach(AttackScript attack in attacks)
        {
            attack.SetTarget(player);
            StartCoroutine(attack.AttemptAttack());
        }
    }

    void Update()
    {
        if (gm.gameIsOn && alive)
        {
            aiPath.maxSpeed = currSpeed;
            RotateSprite(aiPath.velocity.x);
        }
    }

    protected override void AfterDeath()
    {
        aiPath.maxSpeed = 0;
        Destroy(gameObject, 1.5f);
    }

    IEnumerator CheckPlayerProximity()
    {
        while (!aggroed && (Mathf.Abs((player.transform.position - transform.position).magnitude) > aggroRange))
        {
            yield return new WaitForSeconds(0.5f);
        }
        currSpeed = defaultSpeed;
    }

    public override void Damaged()
    {
        aggroed = true;
    }
}
