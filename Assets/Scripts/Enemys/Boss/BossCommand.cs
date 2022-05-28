using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BossCommand : MonoBehaviour
{
    /*
    //?A?^?b?N??????????
    private bool isAttacking;
    public bool IsAttacking => isAttacking;
    */

    protected bool canAttack;
    public bool CanAttack => canAttack;

    //?U???p?????[?^
    [SerializeField] protected float attackCooltime;
    protected float attackTimeCount;

    [SerializeField] protected int attackPower;

    //?U?????K?v???R???|?[?l???g?Q
    [SerializeField] protected Collider2D attackColl;

    private void Update()
    {
        attackTimeCount += Time.deltaTime;
        if(attackTimeCount > attackCooltime)
        {
            canAttack = true;            
        }
    }

    public virtual async UniTask Execute(CancellationToken token)
    {
        await UniTask.Yield();
        attackTimeCount = 0;
        canAttack = false;
    }


}
