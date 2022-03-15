using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤーの本体を表すコンポーネント
    public sealed class PlayerCore : MonoBehaviour
    {
        // // 死んでいるか
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>();

        //プレイヤーのHP
        //細かい仕様は決まってない
        public IReadOnlyReactiveProperty<int> Hp => _playerHP;
        private readonly ReactiveProperty<int> _playerHP = new ReactiveProperty<int>();
        //プレイヤーの攻撃力
        //細かい仕様は決まってない
        public IReadOnlyReactiveProperty<int> PlayerAttackPower => _playerATK;
        [SerializeField] private readonly IntReactiveProperty _playerATK = new IntReactiveProperty(1);


        //ドロエナジー
        //細かい仕様が決まってません。
        //とりあえず、最大値を10として、10で召喚可能。10になるまで貯める必要がある。という仕様にしてます。
        public IReadOnlyReactiveProperty<int> PlayerDrawEnergy => _drawenergy;
        private readonly ReactiveProperty<int> _drawenergy = new ReactiveProperty<int>();

        //外部参照
        PlayerBuff _playerbuff;


        void Start(){
            //キャッシュ
            _playerbuff = PlayerBuff.I;

            //仮でプレイヤーのHPを100としてます。
            _playerHP.Value = 100;
            _playerHP.AddTo(this);

            _playerATK.AddTo(this);
            _drawenergy.Value =10;
            _drawenergy.AddTo(this);

        }

        public void Damaged(int Damage)
        {
            Damage = _playerbuff.Guard(Damage);
            _playerHP.Value -=Damage;
        }

        //毒や効果によるHP減少などの定数ダメージ
        public void DirectDamaged(int Damage){
            _playerHP.Value -=Damage;
        }

        //攻撃力の変更
        public void ChangeAttackPower(int a){
            _playerATK.Value = a;
        }

        public void AddDrawEnergy(){
            _drawenergy.Value += 1;
        }

        public void InitDrawEnergy(){
            _drawenergy.Value = 0;
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
