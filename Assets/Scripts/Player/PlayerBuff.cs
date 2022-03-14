using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Players{
    public class PlayerBuff : MonoBehaviour
    {
        [SerializeField] GuardKunManager guardkunmanager;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public int Buff(int damage){
            //今はガードくんのダメージ軽減だけ
            if(guardkunmanager.IsActive){
                guardkunmanager.Guard(ref damage);
            }
            
            return damage;
        }
    }
}
