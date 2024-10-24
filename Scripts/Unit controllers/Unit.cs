using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float defaultSpeed;
    protected List<float> speedMultipliers;
    protected float currSpeed;
    protected float aimOffSet;
    [SerializeField] protected bool facingRight;

    protected bool alive = true;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected AudioSource audioSource;
    public List<AudioClip> audioClips;
    protected AttackScript[] attacks;

    protected GameManager gm;
    protected SpriteRenderer spriteRenderer;
    
    protected virtual void Awake()
    {
        Init();
    }

    protected void Init()
    {
        speedMultipliers = new List<float>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        currSpeed = defaultSpeed;
        attacks = GetComponentsInChildren<AttackScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator BlockMove(float multiplier, float time)
    {
        speedMultipliers.Add(multiplier);
        float finalSpeed = defaultSpeed;
        foreach (float mul in speedMultipliers)
        {
            finalSpeed *= mul;
        }
        currSpeed = finalSpeed;
        yield return new WaitForSeconds(time);
        speedMultipliers.Remove(multiplier);
        finalSpeed = defaultSpeed;
        foreach (float mul in speedMultipliers)
        {
            finalSpeed *= mul;
        }
        currSpeed = finalSpeed;
    }

    public void Die()
    {
        alive = false;
        currSpeed = 0;
        StopAllCoroutines();
        animator.SetTrigger("died_trig");
        Destroy(rb);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        AfterDeath();
    }

    protected virtual void AfterDeath()
    {

    }

    protected void RotateSprite(float speed)
    {
        if (speed > 0.001f)
        {
            spriteRenderer.flipX = (facingRight) ? false : true;
        }
        if (speed < -0.001f)
        {
            spriteRenderer.flipX = (facingRight) ? true : false;
        }
    }

    public virtual void Damaged() { }
}
