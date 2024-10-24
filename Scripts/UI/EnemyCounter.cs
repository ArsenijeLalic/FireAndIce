using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    private int counter = 1;
    [SerializeField] TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        counter = enemies.Length;
        text.text = "Enemies: " + counter;
        if(counter == 0)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().Victory();
        }
    }
}
