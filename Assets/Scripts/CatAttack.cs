using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int direction_int = 0;
            float direction = (collision.transform.position.x - transform.position.x);
            if (direction > 0)
            {
                direction_int = 1;
            }
            else
            {
                direction_int = -1;
            }
            collision.gameObject.GetComponent<EnemyController>().GetHit(5, true,direction_int);
        }
    }

    
}
