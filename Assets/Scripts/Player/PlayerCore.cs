using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤーの本体を表すコンポーネント
    //FIXME責任過多
    public sealed class PlayerCore : Singleton<PlayerCore>,IApplyDamage,IFallingIsRespown
    {
        //無敵
        private readonly ReactiveProperty<bool> _isInvincible = new ReactiveProperty<bool>(false);

        //行動不能
        public IReadOnlyReactiveProperty<bool> UnMoveable => _unMoveable;
        private readonly ReactiveProperty<bool> _unMoveable = new ReactiveProperty<bool>();

        //プレイヤーのHP
        public IReadOnlyReactiveProperty<int> Hp => _playerHP;
        private readonly ReactiveProperty<int> _playerHP = new ReactiveProperty<int>(100);

        //プレイヤーの攻撃力
        public IReadOnlyReactiveProperty<int> PlayerAttackPower => _playerATK;
        [SerializeField] private readonly IntReactiveProperty _playerATK = new IntReactiveProperty(10);


        //ドローエナジー
        public IReadOnlyReactiveProperty<int> PlayerDrawEnergy => _playerdrawenergy;
        private readonly ReactiveProperty<int> _playerdrawenergy = new ReactiveProperty<int>(10);

        //ポーズ状態
        public IReadOnlyReactiveProperty<bool> PauseState => _pauseState;
        private readonly ReactiveProperty<bool> _pauseState = new ReactiveProperty<bool>(false);

        //外部参照
        private PlayerBuff _playerbuff;
        private PlayerAnimation _playerAniamtion;
        [SerializeField] private CapsuleCollider2D _hitCollider;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] GameObject _player;
        

        [SerializeField]private int MAXDrawEnergy=10;


        void Start(){
            //キャッシュ
            _playerbuff = GetComponent<PlayerBuff>();
            _playerAniamtion = GetComponent<PlayerAnimation>();

            //死んだらシーン移動
            _playerHP
            .Where(x => x < 1)
            // .Subscribe(_ => Debug.Log("脂肪"))
            .Subscribe(_ => SceneController.I.GameOver())
            .AddTo(this);

            //ポーズボタンを感知
            NestedParadox.Players.PlayerInput _playerinput = GetComponent<PlayerInput>();
            _playerinput.OnPause
            .Subscribe(_ => {
                _pauseState.Value = _pauseState.Value == true ? false:true;
                _unMoveable.Value = UnMoveable.Value == true ? false:true;
                _hitCollider.enabled = _hitCollider.enabled == true ? false:true;
            })
            .AddTo(this);
        }

        public void Damaged(Damage _damage)
        {            
            var dame = _playerbuff.Guard( _damage.DamageValue);
            _playerHP.Value -=dame;
            // Debug.Log(_playerHP.Value);

            _playerAniamtion.Damaged();
            //しばらく無敵に
            Invincible(1000).Forget();
        }

        private async UniTask Invincible(int delay){
            _hitCollider.enabled = false;
            await UniTask.Delay(delay);
            _hitCollider.enabled = true;
        }

        //毒や効果によるHP減少などの定数ダメージ
        public void DirectDamaged(int Damage){
            _playerHP.Value -=Damage;
        }

        public void ChangeAttackPower(int atk){
            _playerATK.Value = atk;
        }

        public void AddDrawEnergy(int d){
            _playerdrawenergy.Value += d;
            _playerdrawenergy.Value = Mathf.Clamp(_playerdrawenergy.Value, 0, MAXDrawEnergy);
        }

        public void ResetDrawEnergy(){
            _playerdrawenergy.Value=0;
        }

        public void Respown(){
            // Debug.Log("落ちた");
            //復活処理とダメージ処理を書く
            //無敵

            //エネミームービングにある
            
            //落下を止める
            transform.position = new Vector3(transform.position.x, -2.9f, 0);
            //物理演算を止める
            rb.isKinematic = true;        
            rb.velocity = Vector3.zero;

            //操作・被ダメ不可
            Invincible(1000).Forget();

            //エフェクトを再生。今は無し
            //nearrestPlaceに一番近いスポーン地点が入る
            // GameObject respawnEffect_clone = Instantiate(respawnEffect, transform.position, Quaternion.identity);
            // respawnEffect_clone.transform.SetParent(transform);
            // await UniTask.Delay(1000);

            //ベースフィールドを探す
            GameObject[] baseFields = GameObject.FindGameObjectsWithTag("BaseField");
            float distance = 10000;
            Vector3 nearestPlace = new Vector3();
            foreach(GameObject baseField in baseFields)
            {
                float distance_temp = (transform.position - baseField.transform.position).magnitude;
                if(distance_temp < distance)
                {
                    distance = distance_temp;
                    nearestPlace = baseField.transform.position;
                }
            }

            //復活
            Vector3 respawnPosition = new Vector3(nearestPlace.x, nearestPlace.y + 3, nearestPlace.z);        
            transform.position = respawnPosition;


            //移動用の処理を戻す
            rb.isKinematic = false;

            
            _playerHP.Value -= 10;
            Invincible(1000).Forget();
        }

        //無敵状態を操作
        public void SetInvincible(bool isInvincible)
        {
            _isInvincible.Value = isInvincible;
        }

        public void SetUnMoveable(bool b)
        {
            _unMoveable.Value = b;
        }

        //ポーズ終了
        public void EndPause()
        {
            _pauseState.Value =false;
            // Debug.Log(_pauseState.Value);
        }    
    }
}
