using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤーの本体を表すコンポーネント
    public sealed class PlayerCore : Singleton<PlayerCore>
    {
        // // 死んでいるか
        // public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        // private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>();

        //プレイヤーのHP
        public IReadOnlyReactiveProperty<int> Hp => _playerhp;
        private readonly ReactiveProperty<int> _playerhp = new ReactiveProperty<int>();

        //プレイヤーの攻撃力
        public IReadOnlyReactiveProperty<int> PlayerAttackPower => _playerATK;
        [SerializeField] private readonly IntReactiveProperty _playerATK = new IntReactiveProperty(1);


        //ドローエナジー
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

        public void AddDrawEnergy(int d){
            _playerdrawenergy.Value += d;
        }

        public void ResetDrawEnergy(){
            _playerdrawenergy.Value=0;
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
