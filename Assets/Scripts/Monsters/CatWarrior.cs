using System.Collections;
using System.Collections.Generic;
using NestedParadox.Players;
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
        private float attackTime;        
        private PlayerCore player;
        [SerializeField] float attackSpeed;
        [SerializeField] float attackRecoilSpeed;

        //エフェクト, UI
        [SerializeField] GameObject teleportationEffect;
        [SerializeField] GameObject attackEffect;
        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite reverseSprite;
        [SerializeField] SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            attackPower = 1;
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x, distanceOffset.y, distanceOffset.z);
            Vector3 localScale_temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerCore>();
            state = MonsterState.Idle;
            attackTime = 0;
            attackColl.OnTriggerEnter2DAsObservable().Where(other => !other.CompareTag("Monster")).Subscribe(other => OnHit(other)).AddTo(this);
            player.CurrentDirection.Subscribe(x =>
            {
                if (x == 1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x, distanceOffset_temp.y, distanceOffset_temp.z);
                    spriteRenderer.sprite = normalSprite;
                }
                else if (x == -1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x * -1, distanceOffset_temp.y, distanceOffset_temp.z);
                    spriteRenderer.sprite = reverseSprite;
                }
            }).AddTo(this);
            //プレイヤーのhpによってステータスが変わる。
            player.Hp.Subscribe(x => ChangeAttackPower(x)).AddTo(this);
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
            if (state == MonsterState.Idle)//待機中
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x - distanceOffset.x, 0.1f),
                                                 Mathf.Lerp(transform.position.y, player.transform.position.y - distanceOffset.y, 0.1f),
                                                 Mathf.Lerp(transform.position.z, player.transform.position.z - distanceOffset.z, 0.1f));
            }
            else if (state == MonsterState.Attack)//攻撃中
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
            state = MonsterState.Attack;
            rb.velocity = new Vector3(0, attackSpeed, 0);
            await UniTask.WaitUntil(() => state == MonsterState.Idle, cancellationToken: this.GetCancellationTokenOnDestroy());
            rb.velocity = Vector3.zero;
            attackColl.enabled = false;            
            state = MonsterState.Idle;
        }

        private async void OnHit(Collider2D other)
        {
            Debug.Log("猫戦士の攻撃がヒットしました");
            EnemyBase enemy;
            other.TryGetComponent<EnemyBase>(out enemy);
            if (enemy != null)
            {
                Instantiate(attackEffect, enemy.transform.position, Quaternion.Euler(-50, -90, 90));
                enemy.Damaged(attackPower);
            }
            rb.velocity = (new Vector3(0, attackRecoilSpeed, 0));
            await UniTask.Delay(500, cancellationToken: this.GetCancellationTokenOnDestroy());
            state = MonsterState.Idle;
        }

        public override void Damaged(int damage)
        {
            hp -= damage;
        }

        private void ChangeAttackPower(int playerHp)
        {
            attackPower = (101 - playerHp) / 10;
            if (attackPower == 0)
            {
                attackPower = 1;
            }
            Debug.Log($"猫戦士の攻撃力が{attackPower}になりました(hp={playerHp}");
            spriteRenderer.material.EnableKeyword("_EMISSION");
            float intensity = Mathf.Pow(2, attackPower/2.0f);
            spriteRenderer.material.SetColor("_Color", Color.red*intensity);
        }
    }    
}