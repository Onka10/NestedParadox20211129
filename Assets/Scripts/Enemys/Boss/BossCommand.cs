using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BossCommand : MonoBehaviour
{
    /*
    //�A�^�b�N�����ǂ���
    private bool isAttacking;
    public bool IsAttacking => isAttacking;
    */

    protected bool canAttack;
    public bool CanAttack => canAttack;

    //�U���p�����[�^
    [SerializeField] protected float attackCooltime;
    protected float attackTimeCount;

    //�U���ɕK�v�ȃR���|�[�l���g�Q
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
