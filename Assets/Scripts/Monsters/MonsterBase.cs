using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;


namespace NestedParadox.Monsters
{
    public abstract class MonsterBase : MonoBehaviour
    {
        [SerializeField] protected int hp;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int knockBackValue;
        [SerializeField] protected bool isUniquePosition;//プレイヤーの後ろについていかない場合はtrue
        [SerializeField] private float summonWaitTime; //召喚時の待機時間
        public bool IsInActive;
        protected Vector3 distanceOffset;
        protected MonsterState state;
        public bool IsUniquePosition => isUniquePosition;
        protected ReactiveProperty<int> hp_r = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Hp => hp_r;
        
        // Start is called before the first frame update
        void Awake()
        {
            hp_r.Value = hp;
            IsInActive = false;
        }

        void Update()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {

        }

        public virtual void Damaged(Damage damage)
        {

        }        

        public virtual void SetPositionAndInitialize(Vector3 distanceOffset)
        {            
            this.distanceOffset = distanceOffset;
        }
    }



}