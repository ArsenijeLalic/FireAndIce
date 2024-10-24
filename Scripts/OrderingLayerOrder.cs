using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerOrder : MonoBehaviour
{
    SpriteRenderer spRend;
    // Start is called before the first frame update
    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spRend.sortingOrder = Mathf.RoundToInt(-transform.position.y);
    }
}
