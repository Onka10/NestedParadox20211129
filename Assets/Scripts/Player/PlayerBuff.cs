using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

using NestedParadox.Monsters;

namespace NestedParadox.Players{
    public class PlayerBuff : Singleton<PlayerBuff>
    {
        public IReadOnlyReactiveProperty<int> EnhancedATK => _enhancedATK;
        private readonly ReactiveProperty<int> _enhancedATK = new ReactiveProperty<int>();

        //外部参照
        GuardKunManager _guardkunmanager;

        void Start()
        {
            _guardkunmanager=GuardKunManager.I;
        }

        public int Guard(int damage){
            //今はガードくんのダメージ軽減だけ
            if(_guardkunmanager.IsActive){
                _guardkunmanager.Guard(ref damage);
            }
            
            return damage;
        }

        public void EnhanceATK(int atk){
            _enhancedATK.Value = atk;
            //何秒か待つ? Unitask
            //エンハンスを元に戻す
        }
    }
}
