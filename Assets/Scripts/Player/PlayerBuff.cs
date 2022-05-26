using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

using NestedParadox.Monsters;

namespace NestedParadox.Players{
    public class PlayerBuff : Singleton<PlayerBuff>
    {
        public IReadOnlyReactiveProperty<int> EnhancedATK => _enhancedATK;
        private readonly ReactiveProperty<int> _enhancedATK = new ReactiveProperty<int>();

        void Start(){
        }

        public int Guard(int damage){
            //今はガードくんのダメージ軽減だけ
            if(GuardKunManager.I.Count > 0){
                GuardKunManager.I.Guard(ref damage);
            }
            
            return damage;
        }

        public void EnhanceATK(int atk){
            _enhancedATK.Value = atk;
            Debug.Log("エンハンス");
        }
    }
}
