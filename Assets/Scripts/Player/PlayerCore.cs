using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤーの本体を表すコンポーネント
    public sealed class PlayerCore : Singleton<PlayerCore>,IApplyDamage,IFallingIsRespown
    {
        // // 死んでいるか
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>();

        // 無敵状態
        private bool _isInvincible;

        //プレイヤーのHP
        public IReadOnlyReactiveProperty<int> Hp => _playerhp;
        private readonly ReactiveProperty<int> _playerhp = new ReactiveProperty<int>(2);

        //プレイヤーの攻撃力
        public IReadOnlyReactiveProperty<int> PlayerAttackPower => _playerATK;
        [SerializeField] private readonly IntReactiveProperty _playerATK = new IntReactiveProperty(1);


        //ドローエナジー
        public IReadOnlyReactiveProperty<int> PlayerDrawEnergy => _playerdrawenergy;
        private readonly ReactiveProperty<int> _playerdrawenergy = new ReactiveProperty<int>(0);

        //外部参照
        private PlayerBuff _playerbuff;
        private PlayerAnimation _playeraniamtion;
        [SerializeField] private CapsuleCollider2D _hitCollider;

        [SerializeField]private int MAXDraweEnergy=10;


        void Start(){
            //キャッシュ
            _playerbuff = GetComponent<PlayerBuff>();
            _playeraniamtion = GetComponent<PlayerAnimation>();

            //死んだら死ぬ変数をtrueに
            _playerhp
            .Where(x => x < 0)
            .Subscribe(_ => _isDead.Value = true)
            .AddTo(this);
        }

        public void Damaged(Damage _damage)
        {
            int dame;
            dame = _playerbuff.Guard( _damage.DamageValue);
            _playerhp.Value -=dame;

            _playeraniamtion.Damaged();
            //しばらく無敵に
            Invincible(1000).Forget();
        }

        public async UniTask Invincible(int delay){
            _hitCollider.enabled = false;
            await UniTask.Delay(delay);
            _hitCollider.enabled = true;
        }

        //毒や効果によるHP減少などの定数ダメージ
        public void DirectDamaged(int Damage){
            _playerhp.Value -=Damage;
        }

        public void ChangeAttackPower(int atk){
            _playerATK.Value = atk;
        }

        public void AddDrawEnergy(int d){
            // _playerdrawenergy.Value += d;
            _playerdrawenergy.Value = Mathf.Clamp(_playerdrawenergy.Value += d, 0, MAXDraweEnergy);
        }

        public void ResetDrawEnergy(){
            _playerdrawenergy.Value=0;
        }

        public void Respown(){
            //復活処理とダメージ処理を書く
            Debug.Log("落下！");
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
