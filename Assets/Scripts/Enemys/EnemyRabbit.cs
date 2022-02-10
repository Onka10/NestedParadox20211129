using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

public class EnemyRabbit : EnemyBase, IApplyDamage
{
    
    private readonly ReactiveProperty<float> attackTime = new ReactiveProperty<float>();    
    private bool canAttack;
    private TempCharacter tempCharacter;
    public bool CanAttack { get { return canAttack; } }
    [SerializeField] float attackSpan;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Collider2D bodyColl;
    [SerializeField] Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        attackPower = 1;
        attackTime.Value = 0;
        state.Value = EnemyState.Idle;       
        tempCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        //攻撃用のコライダーに衝突した時、プレイヤーにダメージを与える。
        attackCollider.OnTriggerEnter2DAsObservable()
                      .Where(collision => collision.gameObject.CompareTag("MainCharacter"))
                      .Subscribe(collision =>
                      {
                          collision.gameObject.GetComponent<TempCharacter>().Damaged(attackPower);
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
        state.Value = EnemyState.Attack;
        attackTime.Value = 0;
        if(transform.position.x > tempCharacter.transform.position.x)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }        
        animator.SetTrigger("AttackTrigger");
        await UniTask.Delay(1300);
        attackCollider.enabled = true;
        await UniTask.Delay(100);
        attackCollider.enabled = false;
        await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"), cancellationToken: this.GetCancellationTokenOnDestroy());        
        state.Value = EnemyState.Idle;
    }

    public override void Damaged(int damage)
    {
        Debug.Log("敵にダメージを与えました");
    }

    public void Jump()
    {

    }
}
