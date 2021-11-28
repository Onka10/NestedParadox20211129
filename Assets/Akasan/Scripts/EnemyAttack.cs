using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("monsterAttack"))
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<CharacterMove>().IsCasual)
            {
                enemy.GetComponent<EnemyController>().StopMove();
                collision.gameObject.GetComponent<CharacterMove>().IsCasual = false;
                return;
            }
            collision.gameObject.GetComponent<CharacterMove>().GetHit();
        }
        else if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "GardKun")
        {
            collision.gameObject.GetComponent<MonsterBase>().GetHit();
        }
    }
}
