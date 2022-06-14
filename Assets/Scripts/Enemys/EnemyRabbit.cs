using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Players;

public class EnemyRabbit : EnemyBase, IApplyDamage
{

    private readonly ReactiveProperty<float> attackTime = new ReactiveProperty<float>();
    private bool canAttack;
    private PlayerMove pyerMove;
    public bool CanAttack { get { return canAttack; } }
    [SerializeField] float attackSpan;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Collider2D bodyColl;
    [SerializeField] Animator animator;
    [SerializeField] EnemyMoving enemyMoving;
    public bool IsAttacking => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    public bool IsGetHitting => animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death");
    public bool IsDestroying => animator.GetCurrentAnimatorStateInfo(0).IsName("Death");
    //エフェクト軍
    [SerializeField] GameObject deathEffect;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();                
    }

    void Start()
    {
        attackTime.Value = 0;
        state.Value = EnemyState.Idle;
        pyerMove = PlayerMove.I;
        //攻撃用のコライダーに衝突した時、プレイヤーにダメージを与える。
        attackCollider.OnTriggerEnter2DAsObservable()
                      .Where(collision => collision.gameObject.CompareTag("MainCharacter"))
                      .Subscribe(collision =>
                      {
                          if(collision.TryGetComponent<PlayerCore>(out PlayerCore playerCore))
                          {
                              playerCore.Damaged(new DamageToPlayer(attackPower, 0));
                          }                          
                      })
                      .AddTo(this);
        //攻撃のクールタイムが終わったら、CanAttackをtrueにする。
        attackTime.Select(x => x > attackSpan)
                  .Subscribe(x =>
                  {
                      canAttack = x;
                  })
                  .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        attackTime.Value += Time.deltaTime;
    }

    public override async void Attack()
    {
        Debug.Log("攻撃開始");
        state.Value = EnemyState.Attack;
        attackTime.Value = 0;
        if (transform.position.x > pyerMove.transform.position.x)
        {
            enemyMoving.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            enemyMoving.transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetTrigger("AttackTrigger");
        await UniTask.Delay(866);
        if(!IsAttacking)
        {
            return;
        }
        attackCollider.enabled = true;
        await UniTask.Delay(200);
        attackCollider.enabled = false;
        await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"), cancellationToken: this.GetCancellationTokenOnDestroy());       
        state.Value = EnemyState.Idle;
    }

    public override async void Damaged(Damage damage)
    {
        if(!IsAttacking)
        {
            animator.SetTrigger("GetHitTrigger");
        }        
        hp_r.Value -= damage.DamageValue;        
        if (hp_r.Value <= 0)
        {
            await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"));
            Death();
            return;//早期リターン
        }
    }

    public override async void Death()
    {
        if(IsDestroying)
        {
            return;
        }

        base.Death();
        animator.SetTrigger("DeathTrigger");
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        Instantiate(deathEffect, enemyMoving.transform.position, Quaternion.identity);        
        Destroy(this.gameObject);
    }
}
