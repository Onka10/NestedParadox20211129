using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Monsters
{
    public abstract class MonsterBase : MonoBehaviour
    {
        [SerializeField] protected int hp;
        [SerializeField] protected int attackPower;        
        [SerializeField] protected bool isUniquePosition;//プレイヤーの後ろについていかない場合はtrue
        [SerializeField] private float summonWaitTime; //召喚時の待機時間
        protected Vector3 distanceOffset;
        protected MonsterState state;
        public bool IsUniquePosition => isUniquePosition;
        public int Hp { get { return hp; } }
        
        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {

        }

        public virtual void Damaged(int damage)
        {
            
        }
        

        public virtual void SetPositionAndInitialize(Vector3 distanceOffset)
        {            
            this.distanceOffset = distanceOffset;
        }
    }



}