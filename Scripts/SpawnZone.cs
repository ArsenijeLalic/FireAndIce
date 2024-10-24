using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] public int holdsEnemies;
    private int spawnLimit;
    private float offSet = 0.5f;

    private void Awake()
    {
        spawnLimit = holdsEnemies;
    }

    private void Spawn()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        Vector2 randomOffset = new Vector2(Random.Range(-offSet, offSet), Random.Range(-offSet, offSet));
        Instantiate(enemyPrefabs[index], (Vector2)transform.position+randomOffset, enemyPrefabs[index].transform.rotation);
        spawnLimit--;
    }

    public void Activate() 
    {
        while (spawnLimit > 0)
        {
            Spawn();
        }
    }
}
