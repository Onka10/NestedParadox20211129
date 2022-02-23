using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤーの本体を表すコンポーネント
    public sealed class PlayerCore : MonoBehaviour
    {
        // // 死んでいるか
        // public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        // private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>();

        //プレイヤーのHP
        public IReadOnlyReactiveProperty<int> PlayerHP => _playerhp;
        private readonly ReactiveProperty<int> _playerhp = new ReactiveProperty<int>();

        void Start(){
            //仮でプレイヤーのHPを100としてます。
            _playerhp.Value = 100;
        }


        // 無敵か
    //     private bool _isInvincible;

    //     private void Start()
    //     {
    //         _isDead.AddTo(this);

    //         // 敵に衝突した場合は死ぬ
    //         this.OnCollisionEnter2DAsObservable()
    //             .Where(_ => !_isInvincible)
    //             .Where(x => x.gameObject.TryGetComponent<EnemyCore>(out _))
    //             .Subscribe(onNext: _ => _isDead.Value = true);
    //     }

    //     public void SetInvincible(bool isInvincible)
    //     {
    //         _isInvincible = isInvincible;
    //     }
    // }
    }
}
