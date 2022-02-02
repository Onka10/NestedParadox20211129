using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

public class EnemyRabbit : EnemyBase
{
    
    private readonly ReactiveProperty<float> attackTime = new ReactiveProperty<float>();
    private bool canAttack;
    private TempCharacter tempCharacter;
    public bool CanAttack { get { return canAttack; } }
    [SerializeField] float attackSpan;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        attackPower = 1;
        attackTime.Value = 0;        
        animator = transform.Find("EnemyRabbit").GetComponent<Animator>();
        tempCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        //攻撃用のコライダーに衝突した時、プレイヤーにダメージを与える。
        attackCollider.OnTriggerEnter2DAsObservable()
                      .Where(collision => collision.gameObject.CompareTag("MainCharacter"))
                      .Subscribe(collision =>
                      {
                          collision.gameObject.GetComponent<TempCharacter>().DamageApply(attackPower);
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
        if((transform.position - tempCharacter.transform.position).magnitude < 2)
        {
            attackTime.Value += Time.deltaTime;
        }
    }

    public override async void Attack()
    {
        attackTime.Value = 0;
        if(transform.position.x > tempCharacter.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetTrigger("AttackTrigger");
        attackCollider.enabled = true;
        await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"), cancellationToken: this.GetCancellationTokenOnDestroy());
        attackCollider.enabled = false;
    }

    public override void DamageApply(int damage)
    {
        
    }

    public void Jump()
    {

    }
}
