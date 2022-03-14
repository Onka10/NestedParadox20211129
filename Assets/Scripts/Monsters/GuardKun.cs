using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

namespace NestedParadox.Monsters
{
    public class GuardKun : MonsterBase, IApplyDamage
    {
        [SerializeField] GameObject guardEffect;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] float movingSpeed;
        [SerializeField] Vector3 guardPosition;        
        [SerializeField] Collider2D guardColl;
        private TempCharacter player;
        private MonsterState state;

        // Start is called before the first frame update
        void Start()
        {
            state = MonsterState.Idle;
            player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
            Vector3 localScale_temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x, distanceOffset.y, distanceOffset.z);
            player.CurrentDirection.Subscribe(x =>
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

        // Update is called once per frame
        void FixedUpdate()
        {
            if (state == MonsterState.Idle)//待機中
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x - distanceOffset.x, 0.1f),
                                                 Mathf.Lerp(transform.position.y, player.transform.position.y - distanceOffset.y, 0.1f),
                                                 Mathf.Lerp(transform.position.z, player.transform.position.z - distanceOffset.z, 0.1f));
            }
            else if (state == MonsterState.Guard)//ガード中
            {

            }
        }

        public async void Guard()
        {
            state = MonsterState.Guard;
            transform.position = player.transform.position;
            hp -= 1;            
            GameObject guardEffect_clone = Instantiate(guardEffect, transform.position, Quaternion.identity);
            if(transform.localScale.x < 0)
            {
                guardEffect_clone.transform.localScale = new Vector3(guardEffect_clone.transform.localScale.x*-1,
                                                                   guardEffect_clone.transform.localScale.y,
                                                                   guardEffect_clone.transform.localScale.z);
            }
            await UniTask.Delay(1000);
            state = MonsterState.Idle;            
        }

        public override void SetPositionAndInitialize(Vector3 distanceOffset)
        {
            base.SetPositionAndInitialize(distanceOffset);
            GameObject.Find("GuardKunManager").GetComponent<GuardKunManager>().Add(this);
        }

    }   
}
