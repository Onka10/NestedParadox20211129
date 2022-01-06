using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float DestroyTime;
    // Start is called before the first frame update
    void Start()
    {
        DestroyTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTime += Time.deltaTime;
        if(DestroyTime > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
