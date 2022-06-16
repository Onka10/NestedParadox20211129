using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using NestedParadox.Players;
using System.Threading;

public class BodyAttack : BossCommand
{
    //コンポーネント群
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    //パラメータ群
    [SerializeField] float bodyAttackVelocity; //bodyAttackのスピード
    [SerializeField] float backVelocity; //最初に下がる初速
    [SerializeField] float returnVelocity; //最後に戻る初速
    [SerializeField] float attackStartTime; //攻撃し始める始める時間
    [SerializeField] int attackEndDelayTime; //戻り始める時間
    [SerializeField] Vector3 firstPosition; //ボスの最初の位置;

    public override async UniTask Execute(CancellationToken token)
    {
        await base.Execute(token);
        //アタック子ライダーの当たり判定を購読
        attackColl.OnTriggerEnter2DAsObservable()
            .Where(other => other.CompareTag("MainCharacter"))
            .Subscribe(other =>
            {
                if (other.TryGetComponent<PlayerCore>(out PlayerCore playerCore))
                {
                    playerCore.Damaged(new DamageToPlayer(attackPower, 0));
                }
            })
            .AddTo(this);
            

        animator.SetTrigger("BodyAttackTrigger");

        //後ろに下がって攻撃の準備
        float timeCount = 0;
        while (timeCount < attackStartTime)
        {
            rb.velocity = new Vector2(backVelocity, 0) - rb.velocity;
            timeCount += Time.fixedDeltaTime;
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);
        }

        //攻撃開始
        rb.velocity = new Vector2(bodyAttackVelocity, 0);
        attackColl.enabled = true;
        await UniTask.Delay(attackEndDelayTime, cancellationToken: token);
        attackColl.enabled = false;

        //元の位置に戻る
        float firstDistance = (firstPosition - transform.position).magnitude;
        float currentDistance = (firstPosition - transform.position).magnitude;
        while (currentDistance > 0.05f)
        {
            rb.velocity = new Vector2(returnVelocity, 0) * (currentDistance / firstDistance);
            currentDistance = (firstPosition - transform.position).magnitude;
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);
        }
        transform.position = firstPosition;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("IdleTrigger");
    }
}
