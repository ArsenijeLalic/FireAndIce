using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Residue : MonoBehaviour
{
    [SerializeField] float duration = 3.0f;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("delete_trig");
        Destroy(gameObject, 0.34f);
    }
}
