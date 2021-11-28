using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D coll;
    [SerializeField] Image hpGauge;
    GameObject player;
    Transform playerTransform;
    Transform myTransform;
    [SerializeField] float movingPower;
    public Animator animator;
    int hp;
    bool IsHit;
    float attackTime;
    float attackSpan;
    public bool IsStop;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        myTransform = transform;
        hp = 5000;
        attackTime = 0;
        attackSpan = 5;
        IsStop = false;
    }

    void Update()
    {
        if(IsStop)
        {
            return;
        }
        attackTime += Time.deltaTime;
        Vector3 distanceVector = playerTransform.position - myTransform.position;
        float distance = distanceVector.magnitude;
        if(attackTime > attackSpan && distance < 1)
        {
            attackTime = 0;
            animator.SetTrigger("AttackTrigger");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsStop)
        {
            return;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death") )
        {
            return;
        }

        Vector2 movingDirection = playerTransform.position - myTransform.position;
        rb.AddForce(movingDirection * movingPower);
        if((playerTransform.position.x - myTransform.position.x) < 0 )
        {
            myTransform.localScale = new Vector3(0.1827f, 0.1761f, 1);
        }
        else
        {
            myTransform.localScale = new Vector3(-0.1827f, 0.1761f, 1);
        }
    }


    public void GetHit(int damage, bool isBack, int direction)
    {
        animator.SetTrigger("GetHitTrigger");
        hp -= damage;
        hpGauge.fillAmount = hp / 5000.0f;
        coll.enabled = false;
        if(hp <= 0)
        {
            animator.SetTrigger("DeathTrigger");
            Invoke("Destroy", 2.0f);
        }
        else
        {
            if(isBack)
            {
                rb.AddForce(new Vector2(10000*direction, 0));
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void SetCollider()
    {
        coll.enabled = true;
    }

    public void UnsetCollider()
    {
        coll.enabled = false;
    }

    public IEnumerator StopMove()
    {
        IsStop = true;
        yield return new WaitForSeconds(4);
        IsStop = false;
        yield break;
    }
    

}
