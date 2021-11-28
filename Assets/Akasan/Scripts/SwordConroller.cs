using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordConroller : MonoBehaviour
{
    [SerializeField] Collider2D coll;
    [SerializeField] Animator animator;
    [SerializeField] Transform playerTransform;
    GameObject cardManager;
    public int attackValue;
    // Start is called before the first frame update
    void Start()
    {
        cardManager = GameObject.Find("CardManager");
        attackValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCollider()
    {
        coll.enabled = true;
    }

    public void UnsetCollider()
    {
        coll.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int direction_int = 0;
            float direction = (collision.transform.position.x - playerTransform.position.x);
            if (direction > 0)
            {
                direction_int = 1;
            }
            else
            {
                direction_int = -1;
            }
            cardManager.GetComponent<CardManager>().IncreaseDrawEnergy(10);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(10*attackValue, false, 1);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(10*attackValue, false,1);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(10*attackValue, false,1);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(20*attackValue, true, direction_int);
            }
        }
    }
}
