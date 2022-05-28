using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Threading;

public class Omnipotence : EnemyBase
{
    //???????
    [SerializeField] private Animator animator;
    [SerializeField] private Animator getHitAnimator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private OmnipotenceAttack attack;    
    private bool isKnockBacked => animator.GetCurrentAnimatorStateInfo(0).IsName("KnockBack");
    private CancellationTokenSource damagedAnimCts;//?????????????????????
    private CancellationTokenSource attackCts; //アタックのキャンセレーショントークン
    [SerializeField] int knockBackDurableValue; //?????????
    private int currentKnockBackValue; //??????????

    protected override void Awake()
    {
        base.Awake();
        currentKnockBackValue = knockBackDurableValue;
        damagedAnimCts = new CancellationTokenSource();
        attackCts = new CancellationTokenSource();
        attack.CanAttack
            .Where(x => !isKnockBacked && x)
            .Subscribe(_ => Attack())
            .AddTo(this);
    }

    public override async void Damaged(Damage damage)
    {
        await UniTask.Yield();
        hp_r.Value -= damage.DamageValue;
        currentKnockBackValue -= damage.KnockBackValue;
        if(hp_r.Value <= 0)
        {
            Death();
            return;
        }
        Debug.Log($"????:{damage.DamageValue}\n????:{damage.KnockBackValue}\n???????????\nHP:{hp_r.Value},????{currentKnockBackValue}");
        if(currentKnockBackValue <= 0 && !attack.IsAttacking)
        {
            currentKnockBackValue = knockBackDurableValue;
            knockBackAnimation();
            return;
        }
        await DamagedAnimation();
    }

    private async UniTask DamagedAnimation()
    {
        damagedAnimCts.Cancel(); //????DamageAnimation??????
        damagedAnimCts = new CancellationTokenSource();        
        getHitAnimator.SetBool("GetHitBool", true);
        try
        {
            await UniTask.Delay(500, cancellationToken: damagedAnimCts.Token);
        }
        catch
        {                       
            return;
        }
        getHitAnimator.SetBool("GetHitBool", false);
    }

    private async void knockBackAnimation()
    {        
        damagedAnimCts.Cancel();
        animator.SetTrigger("KnockBackTrigger");
        getHitAnimator.SetBool("KnockBackBool", true);
        await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
        await UniTask.WaitUntil(() => !isKnockBacked, cancellationToken: this.GetCancellationTokenOnDestroy());
        getHitAnimator.SetBool("KnockBackBool", false);
    }

    public async override void Death()
    {
        //アタックのキャンセル
        attackCts.Cancel();
        attackCts = new CancellationTokenSource();
        //ノックバックと被弾のキャンセル;
        damagedAnimCts.Cancel();
        getHitAnimator.SetBool("KnockBackTrigger", false);

        animator.SetTrigger("DeathTrigger");
        await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Death"), cancellationToken: this.GetCancellationTokenOnDestroy());
        isDeath.OnNext(Unit.Default);
    }


    public override void Attack()
    {
        Debug.Log("???????");
        attack.Execute(attackCts.Token);
    }
}
