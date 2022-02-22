using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Monsters
{
    public abstract class MonsterBase : MonoBehaviour
    {
        [SerializeField] protected int hp;
        [SerializeField] protected int attackPower;
        [SerializeField] protected bool isUniquePosition;
        protected Vector3 distanceOffset;
        public bool IsUniquePosition => isUniquePosition;
        public int Hp { get { return hp; } }
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
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