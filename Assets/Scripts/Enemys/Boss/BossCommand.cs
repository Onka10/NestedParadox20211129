using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BossCommand : MonoBehaviour
{
    /*
    //アタック中かどうか
    private bool isAttacking;
    public bool IsAttacking => isAttacking;
    */

    protected bool canAttack;
    public bool CanAttack => canAttack;

    //攻撃パラメータ
    [SerializeField] protected float attackCooltime;
    protected float attackTimeCount;

    //攻撃に必要なコンポーネント群
    [SerializeField] protected Collider2D attackColl;

    private void Update()
    {
        attackTimeCount += Time.deltaTime;
        if(attackTimeCount > attackCooltime)
        {
            canAttack = true;            
        }
    }

    public virtual async UniTask Execute()
    {
        await UniTask.Yield();
        attackCooltime = 0;
        canAttack = false;
    }


}
