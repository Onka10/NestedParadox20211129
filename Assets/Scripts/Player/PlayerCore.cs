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
        //細かい仕様は決まってない
        public IReadOnlyReactiveProperty<int> Hp => _playerhp;
        private readonly ReactiveProperty<int> _playerhp = new ReactiveProperty<int>();
        //プレイヤーの攻撃力
        //細かい仕様は決まってない
        public IReadOnlyReactiveProperty<int> PlayerAttackPower => _playerATK;
        [SerializeField] private readonly IntReactiveProperty _playerATK = new IntReactiveProperty(1);

        //ドロエナジー
        //細かい仕様が決まってません。
        //とりあえず、最大値を10として、10で召喚可能。10になるまで貯める必要がある。という仕様にしてます。
        public IReadOnlyReactiveProperty<int> PlayerDrawEnergy => _playerdrawenergy;
        private readonly ReactiveProperty<int> _playerdrawenergy = new ReactiveProperty<int>();

        //外部参照
        PlayerBuff _playerbuff;


        void Start(){
            //キャッシュ
            _playerbuff = PlayerBuff.I;

            //仮でプレイヤーのHPを100としてます。
            _playerhp.Value = 100;
            _playerdrawenergy.Value =10;
        }

        public void Damaged(int Damage)
        {
            Damage = _playerbuff.Guard(Damage);
            _playerhp.Value -=Damage;
        }

        //毒や効果によるHP減少などの定数ダメージ
        public void DirectDamaged(int Damage){
            _playerhp.Value -=Damage;
        }

        public void ChangeAttackPower(int atk){
            _playerATK.Value = atk;
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
