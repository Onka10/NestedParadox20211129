using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private EnemyRabbit enemyRabbit;
    private EnemyMoving enemyMoving;
    // Start is called before the first frame update
    void Start()
    {
        enemyRabbit = GetComponent<EnemyRabbit>();
        enemyMoving = GetComponent<EnemyMoving>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyRabbit.CanAttack)
        {
            Debug.Log("うさぎの攻撃");
            enemyRabbit.Attack();
        }
    }
}
