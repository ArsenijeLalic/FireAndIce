using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().UpdateHealth(2, "holy");
            audioSource.PlayOneShot(clip);
            Destroy(gameObject, 0.1f);
        }
    }
}
