using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Monsters
{
    public class SniperK : MonsterBase, IApplyDamage
    {
        [SerializeField] Animator animator;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] float shotPower;
        [SerializeField] Vector3 shotPosition;
        [SerializeField] float attackSpan;
        private float attackTime;
        private TempCharacter player;
        private MonsterState state;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
            attackTime = 0;
            state = MonsterState.Idle;
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x, distanceOffset.y, distanceOffset.z);
            Vector3 localScale_temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 shotPosition_temp = new Vector3(shotPosition.x, shotPosition.y, shotPosition.z);
            player.CurrentDirection.Subscribe(x =>
            {
                if (x == 1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x, distanceOffset_temp.y, distanceOffset_temp.z);
                    shotPosition = new Vector3(shotPosition_temp.x, shotPosition_temp.y, shotPosition_temp.z);
                    transform.localScale = new Vector3(localScale_temp.x, localScale_temp.y, localScale_temp.z);
                }
                else if (x == -1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x * -1, distanceOffset_temp.y, distanceOffset_temp.z);
                    shotPosition = new Vector3(shotPosition_temp.x*-1, shotPosition_temp.y, shotPosition_temp.z);
                    transform.localScale = new Vector3(localScale_temp.x*-1, localScale_temp.y, localScale_temp.z);
                }
            }).AddTo(this);
        }

        // Update is called once per frame
        void Update()
        {
            attackTime += Time.deltaTime;
            if(attackTime > attackSpan)
            {
                Attack();
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

        public override void Damaged(int damage)
        {
            hp -= damage;
        }

        private void Shot()
        {
            int[] randomAngles = {Random.Range(0, 91), Random.Range(0, 91), Random.Range(0, 91)};           
            foreach(int randomAngle in randomAngles)
            {     
                Vector3 shotVector =  new Vector3(shotPower * Mathf.Cos(Mathf.PI*randomAngle/180.0f), shotPower * Mathf.Sin(Mathf.PI * randomAngle / 180.0f), 0) ;
                if(transform.localScale.x < 0)
                {
                    shotVector = new Vector3(shotVector.x * -1, shotVector.y, shotVector.z);
                }
                GameObject bullet_clone = Instantiate(bulletPrefab, transform.position + shotPosition, Quaternion.identity);
                bullet_clone.GetComponent<Rigidbody2D>().AddForce(shotVector);
                bullet_clone.GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                    .Where(other => !other.CompareTag("MainCharacter") && !other.CompareTag("Monster") && !other.CompareTag("Bullet") && !other.CompareTag("FootHold2"))
                    .Subscribe(other =>
                    {                                                            
                        GameObject explosion_clone = Instantiate(explosionPrefab, bullet_clone.transform.position, Quaternion.identity);
                        explosion_clone.GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                            .Subscribe(other =>
                            {
                                EnemyBase enemy;
                                other.TryGetComponent<EnemyBase>(out enemy);
                                if (enemy != null)
                                {
                                    enemy.Damaged(attackPower);
                                }
                            })
                            .AddTo(explosion_clone);
                        Destroy(bullet_clone);
                    })
                    .AddTo(bullet_clone);
            }            
        }

        private async void Attack()
        {
            state = MonsterState.Attack;
            attackTime = 0;
            animator.SetTrigger("AttackTrigger");
            await UniTask.Delay(1180, cancellationToken: this.GetCancellationTokenOnDestroy());
            Shot();
            state = MonsterState.Idle;
        }
    }    
}