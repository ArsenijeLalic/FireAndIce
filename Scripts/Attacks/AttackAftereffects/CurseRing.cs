using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseRing : MonoBehaviour
{
    [SerializeField] private int dmg;
    [SerializeField] private string element;
    [SerializeField] private float ringLifetime;
    [SerializeField] private float tickRate;
    private bool active = false;
    private GameObject target;
    private HealthSystem targetHS;
    private Animator animator;

    private void Awake()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(StartTicking());
    }

    private void FixedUpdate()
    {
        if (ringLifetime < 0)
        {
            active = false;
            StopAllCoroutines();
            Disappear();
            Destroy(gameObject, 0.42f);
        }
        ringLifetime -= Time.fixedDeltaTime;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(target.tag) && active)
        {
            if (targetHS == null)
                targetHS = target.GetComponent<HealthSystem>();

            targetHS.UpdateHealth(-dmg, element);
            active = false;
        }
    }

    IEnumerator StartTicking()
    {
        while (true)
        {
            active = false;
            yield return new WaitForSeconds(tickRate);
            active = true;
            yield return null;
        }
    }

    private void Disappear()
    {
        animator.SetTrigger("disappear_trig");
    }
}