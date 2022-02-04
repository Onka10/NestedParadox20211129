using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

namespace NestedParadox.Monsters
{
    public class CatWarrior : MonsterBase, IApplyDamage
    {
        [SerializeField] Collider2D attackColl;
        [SerializeField] Animator animator;
        [SerializeField] float attackSpan;
        private float attackTime;
        public CatWarriorState state;
        private TempCharacter player;
        [SerializeField] Vector3 offsetDistance;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
            state = CatWarriorState.Idle;
            attackColl = GetComponent<Collider2D>();
            attackColl.OnTriggerEnter2DAsObservable()
                      .Subscribe(other =>
                      {
                          EnemyBase enemy;
                          other.TryGetComponent<EnemyBase>(out enemy);
                          if (enemy != null)
                          {
                              enemy.Damaged(attackValue);
                              animator.SetTrigger("IdleTriggre");
                          }
                      });
        }

        // Update is called once per frame
        void Update()
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackSpan && GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                Attack();
                attackTime = 0;
            }
        }

        void FixedUpdate()
        {
            if (state == CatWarriorState.Idle)//待機中
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x - offsetDistance.x, 0.05f),
                                                 Mathf.Lerp(transform.position.x, player.transform.position.x - offsetDistance.x, 0.05f),
                                                 Mathf.Lerp(transform.position.x, player.transform.position.x - offsetDistance.x, 0.05f));
            }
            else if (state == CatWarriorState.Attack)//攻撃中
            {

            }
        }

        public async void Attack()
        {
            GameObject[] targetEnemys = GameObject.FindGameObjectsWithTag("Enemy");
            int random = Random.Range(0, targetEnemys.Length);
            Vector3 targetPosition = new Vector3(targetEnemys[random].transform.position.x, 3.5f, 0);
            attackColl.enabled = true;
            state = CatWarriorState.Attack;
            animator.SetTrigger("AttackTrigger");
            await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"), cancellationToken: this.GetCancellationTokenOnDestroy());
            attackColl.enabled = false;
            state = CatWarriorState.Idle;
        }

        public override void Damaged(int damage)
        {

        }
    }

    public enum CatWarriorState
    {
        Idle,
        Attack,
    }
}