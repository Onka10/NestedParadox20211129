using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardKun : MonsterBase
{
    EnemyController enemyController;
    Transform enemyTransform;
    Transform myTransform;
    public Vector3 basePoint;
    int hp;
    [SerializeField] float movingSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D coll;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        hp = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemyController.animator.GetCurrentAnimatorStateInfo(0).IsName("monsterAttack"))
        {
            if(enemyTransform.localScale.x > 0)
            {
                Vector3 moveDirection = enemyTransform.position + new Vector3(-0.4f, -1.7f, 0) - myTransform.position;
                rb.velocity = moveDirection*movingSpeed;
                if(moveDirection.magnitude < 0.5f)
                {
                    coll.isTrigger = false;
                }
            }
            else
            {
                Vector3 moveDirection = enemyTransform.position + new Vector3(0.4f, -1.7f, 0) - myTransform.position;
                rb.velocity = moveDirection*movingSpeed;
                if (moveDirection.magnitude < 0.5f)
                {
                    coll.isTrigger = false;
                }
            }
            
        }
        else
        {
            coll.isTrigger = true;
            myTransform.position = basePoint;
        }
    }

    public override void GetHit()
    {
        animator.SetTrigger("GetHitTrigger");
        hp -= 1;
        if(hp <= 0)
        {
            Invoke("Destroy",1);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
