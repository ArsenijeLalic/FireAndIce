using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected float range;
    [SerializeField] protected int damage;
    [SerializeField] protected string enemyTag;
    [SerializeField] protected float firstCastDelay;
    protected bool onCooldown;
    protected GameManager gm;
    protected GameObject target;

    [SerializeField] protected float rangeCheckerFrequency;

    [SerializeField] protected string stateTrigger;
    protected float attackStaggerDuration = 0.25f;
    private GameObject parent;
    protected Animator animator;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip attackSound;
    protected virtual void Awake()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        parent = GetComponentInParent<Unit>().gameObject;
        animator = parent.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start() {}

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public IEnumerator AttemptAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(rangeCheckerFrequency);
            if (InRange())
            {
                yield return new WaitForSeconds(firstCastDelay);
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
    }

    protected IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    protected Vector2 takeAim()
    {
        Vector2 aimAt;
        if (target == null)
        {
            aimAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            aimAt = target.transform.position;
        }
        return aimAt;
    }

    protected void AttackAnimation()
    {
        animator.SetTrigger(stateTrigger);
        
        StartCoroutine(parent.GetComponent<Unit>().BlockMove(0.8f, attackStaggerDuration));
        
    }

    protected bool InRange()
    {
        return Mathf.Abs((target.gameObject.transform.position - gameObject.transform.position).magnitude) < range;
    }

}
