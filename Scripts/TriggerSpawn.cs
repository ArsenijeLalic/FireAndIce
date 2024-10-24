using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    [SerializeField] SpawnZone[] spawnZones;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggered && collision.CompareTag("Player"))
        {
            foreach(SpawnZone sz in spawnZones)
            {
                sz.Activate();
            }
            triggered = true;
        }
    }
}
