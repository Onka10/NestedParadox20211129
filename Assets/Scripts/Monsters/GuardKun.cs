using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Players;

namespace NestedParadox.Monsters
{
    public class GuardKun : MonsterBase, IApplyDamage
    {
        private GuardKunManager guardKunManager;
        [SerializeField] GameObject guardEffect;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] float movingSpeed;
        [SerializeField] Vector3 guardPosition;        
        [SerializeField] Collider2D guardColl;
        [SerializeField] float speed_MoveAndStop;        
        private bool isActive; //ガードクインテット発動中
        public bool IsActive => isActive;
        private PlayerMove playerMove;
        

        // Start is called before the first frame update
        void Start()
        {
            guardKunManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GuardKunManager>();
            //マネージャーに自身を追加
            guardKunManager.Add(this);
            state = MonsterState.Idle;
            isActive = true;
            playerMove = PlayerMove.I;
            Vector3 localScale_temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x, distanceOffset.y, distanceOffset.z);
            playerMove.CurrentDirection.Subscribe(x =>
            {
                if(x == 1)
                {
                    transform.localScale = new Vector3(localScale_temp.x * -1, localScale_temp.y, localScale_temp.z);
                    distanceOffset = new Vector3(distanceOffset_temp.x, distanceOffset.y, distanceOffset.z);
                }
                else if(x == -1)
                {
                    transform.localScale = new Vector3(localScale_temp.x, localScale_temp.y, localScale_temp.z);
                    distanceOffset = new Vector3(distanceOffset_temp.x * -1, distanceOffset_temp.y, distanceOffset.z);
                }
            });
        }

        protected override void Update()
        {
            lifeTime += Time.deltaTime;
            if (lifeTime > 30)
            {
                GameObject deathEffect_clone = Instantiate(deathEffect);
                deathEffect_clone.transform.position = transform.position;
                Destroy(this.gameObject);
            }
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if(!isActive)
            {
                return;
            }
            if (state == MonsterState.Idle)//待機中
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerMove.transform.position.x - distanceOffset.x, 0.1f),
                                                 Mathf.Lerp(transform.position.y, playerMove.transform.position.y - distanceOffset.y, 0.1f),
                                                 Mathf.Lerp(transform.position.z, playerMove.transform.position.z - distanceOffset.z, 0.1f));
            }
            else if (state == MonsterState.Guard)//ガード中
            {

            }
        }

        public async void Guard()
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.GuardKun);
            state = MonsterState.Guard;
            transform.position = playerMove.transform.position;
            hp_r.Value -= 1;            
            GameObject guardEffect_clone = Instantiate(guardEffect, transform.position, Quaternion.identity);
            if(transform.localScale.x < 0)
            {
                guardEffect_clone.transform.localScale = new Vector3(guardEffect_clone.transform.localScale.x*-1,
                                                                   guardEffect_clone.transform.localScale.y,
                                                                   guardEffect_clone.transform.localScale.z);
            }
            if(hp_r.Value <= 0)
            {
                GameObject deathEffect_clone = Instantiate(deathEffect);
                deathEffect_clone.transform.position = transform.position;
                Destroy(this.gameObject);
            }
            state = MonsterState.Idle;            
        }

        public async UniTask MoveAndStop(Vector3 destination)
        {
            isActive = false;                 
            Vector3 direction = destination - transform.position;            
            while (direction.magnitude > 2.1f)
            {
                rb.velocity = direction * speed_MoveAndStop;
                direction = destination - transform.position;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: this.GetCancellationTokenOnDestroy());
                Debug.Log(direction.magnitude);
            }
            rb.velocity = new Vector3(0, 0, 0);
        }

        public void SetActive()
        {
            isActive = true;
            guardKunManager.Add(this);
        }
    }   
}
