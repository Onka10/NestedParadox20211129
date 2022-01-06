using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    GameObject enemy;
    Transform enemyTransform;
    Rigidbody2D rb;
    Transform myTransform;
    public int attackValue;
    [SerializeField] float movingPower;
    [SerializeField] GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTransform = enemy.transform;
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = (enemyTransform.position - myTransform.position).normalized;
        rb.AddForce(moveDirection * movingPower);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            float direction = enemyTransform.position.x - myTransform.position.x;
            int direction_int = 0;
            if(direction > 0)
            {
                direction_int = 1;
            }
            else
            {
                direction_int = -1;
            }
            collision.gameObject.GetComponent<EnemyController>().GetHit(attackValue, true, direction_int * 2);
            GameObject explosion_clone = Instantiate(explosionPrefab);
            explosion_clone.transform.position = myTransform.position;
            Destroy(gameObject);
        }
    }
}
