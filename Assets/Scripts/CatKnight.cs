using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatKnight : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform myTransform;
    [SerializeField] Animator animator;
    [SerializeField] float movingPower;
    [SerializeField] Collider2D coll;
    float attackTime;
    float attackSpan;
    float deathTime;
    float deathSpan;
    GameObject enemy;
    Transform enemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTransform = enemy.transform;
        attackTime = 0;
        attackSpan = 2;
        deathTime = 0;
        deathSpan = 30;
    }
    void Update()
    {
        deathTime += Time.deltaTime;
        if(deathTime > deathSpan)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("WalkFloat", rb.velocity.magnitude);
        if(enemy == null)
        {
            animator.SetTrigger("IdleTrigger");
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemyTransform = enemy.transform;
            rb.velocity = Vector2.zero;
            return;
        }
        attackTime += Time.deltaTime;
        Vector3 moveDirection = enemyTransform.position - myTransform.position;
        if(moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(moveDirection.magnitude > 2.5f)
        {
            rb.AddForce(moveDirection.normalized * movingPower);
        }
        else
        {
            if(attackTime > attackSpan)
            {
                rb.velocity = Vector2.zero;
                attackTime = 0;
                Debug.Log("アタック");
                animator.SetTrigger("AttackTrigger");
            }
        }
    }

    public void SetCollider()
    {
        coll.enabled = true;
    }
    public void UnsetCollider()
    {
        coll.enabled = false;
    }
}
