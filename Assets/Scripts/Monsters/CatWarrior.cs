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
        [SerializeField] Rigidbody2D rb;
        [SerializeField] GameObject teleportationEffect;
        [SerializeField] GameObject attackEffect;
        private float attackTime;
        public CatWarriorState state;
        private TempCharacter player;        
        [SerializeField] float attackSpeed;
        [SerializeField] float attackRecoilPower;
        // Start is called before the first frame update
        void Start()
        {
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x,distanceOffset.y, distanceOffset.z);
            Vector3 localScale_temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
            state = CatWarriorState.Idle;
            attackTime = 0;            
            attackColl.OnTriggerEnter2DAsObservable().Where(other => !other.CompareTag("Monster")).Subscribe(other => OnHit(other)).AddTo(this);                      
            player.CurrentDirection.Subscribe(x =>
            {
                if(x == 1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x, distanceOffset_temp.y, distanceOffset_temp.z);
                    transform.localScale = new Vector3(localScale_temp.x * -1, localScale_temp.y, localScale_temp.z);
                }
                else if(x == -1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x * -1, distanceOffset_temp.y, distanceOffset_temp.z);
                    transform.localScale = new Vector3(localScale_temp.x , localScale_temp.y, localScale_temp.z);
                }
            }).AddTo(this);
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
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x - distanceOffset.x, 0.1f),
                                                 Mathf.Lerp(transform.position.y, player.transform.position.y - distanceOffset.y, 0.1f),
                                                 Mathf.Lerp(transform.position.z, player.transform.position.z - distanceOffset.z, 0.1f));
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
            transform.position = targetPosition;           
            Instantiate(teleportationEffect, targetPosition, Quaternion.identity);
            attackColl.enabled = true;
            state = CatWarriorState.Attack;
            rb.velocity = new Vector3(0, attackSpeed, 0);
            await UniTask.WaitUntil(() => state == CatWarriorState.Idle, cancellationToken: this.GetCancellationTokenOnDestroy());
            rb.velocity = Vector3.zero;
            attackColl.enabled = false;
            state = CatWarriorState.Idle;
        }

        private async void OnHit(Collider2D other)
        {            
            EnemyBase enemy;
            other.TryGetComponent<EnemyBase>(out enemy);
            if (enemy != null)
            {
                Instantiate(attackEffect, enemy.transform.position, Quaternion.Euler(-50, -90, 90));
                enemy.Damaged(attackValue);
            }
            rb.AddForce(new Vector3(0, attackRecoilPower, 0));
            await UniTask.Delay(500, cancellationToken: this.GetCancellationTokenOnDestroy());
            state = CatWarriorState.Idle;
        }

        public override void Damaged(int damage)
        {
            hp -= damage;
        }
    }

    public enum CatWarriorState
    {
        Idle,
        Attack,
    }
}