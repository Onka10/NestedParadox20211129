using UniRx;
using UniRx.Triggers;
// using UniRxSampleGame.Damages;
using UnityEngine;
using System;

namespace NestedParadox.Players
{
    // プレイヤの攻撃処理の管理コンポーネント
    public class PlayerAttack : MonoBehaviour
    {

        //外部参照
        private PlayerInput _playerinput;
        private PlayerAnimation _playerAnimation;
        private PlayerMove _playerMove;
        private PlayerCore _playercore;

        //購読用に用意する
        private readonly ReactiveProperty<bool> _isInAttack = new ReactiveProperty<bool>(false);

        // ヒエラルキー上で子要素として存在する攻撃判定用コライダ
        [SerializeField] private Collider2D _attackCollider1;
        [SerializeField] private Collider2D _attackCollider2;



        private void Start()
        {
            _playerinput = GetComponent<PlayerInput>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerMove = GetComponent<PlayerMove>();
            _playercore = GetComponent<PlayerCore>();

            // 操作イベントの購読
            SubscribeInputEvent();

            // アニメーションイベントの購読
            SubscribeAnimationEvent();

            // 衝突イベントの購読
            SubscribeColliderEvent();

            // 攻撃中は移動不可フラグを立てる
            _isInAttack
                .Where(x => true)
                .Subscribe(x => _playerMove.BlockMove(x))
                .AddTo(this);

            // OnAttackEndEvent();
        }

        #region 操作イベントの購読
        private void SubscribeInputEvent()
        {
            //一応連打防止の対応したけど、タメ攻撃と通常で分けれて無いし、今後攻撃が増えると通用しなくなる

            // 弱攻撃イベント
            _playerinput.OnNormalAttack
                // 接地中なら攻撃ができる
                .Where(_ => _playerMove.IsGrounded.Value)
                .ThrottleFirst(TimeSpan.FromSeconds(.6))//連打防止。通常攻撃は.4秒
                .Subscribe(_ => NAttack())
                .AddTo(this);

            // 強攻撃イベント
            _playerinput.OnChargeAttack
                // 接地中なら攻撃ができる
                .Where(_ => _playerMove.IsGrounded.Value)
                .ThrottleFirst(TimeSpan.FromSeconds(1))//連打防止。タメ攻撃はあ1秒
                .Subscribe(_ => CAttack())
                .AddTo(this);
        }

        //本来は攻撃が当たったときにドローエナジーを増やして欲しいけど、今はこれで我慢
        private void NAttack(){
            _playerAnimation.NormalAttack();
            OnNormalAttackEvent();
            _playercore.AddDrawEnergy(10);
        }

        private void CAttack(){
            _playerAnimation.ChargeAttack();
            OnChargeAttackEvent();
            _playercore.AddDrawEnergy(2);
        }
        #endregion 

        # region アニメーションイベントを購読する
        private void SubscribeAnimationEvent(){
            // ObservableStateMachineTrigger を用いることでAnimationControllerのステートの遷移を取得できるが
            // 今回はAnimationは普通に実装してるので""使えません""
            var animator = GetComponent<Animator>();
            var trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

            // 攻撃関係のステートマシンに入った
            trigger
                .OnStateUpdateAsObservable()
                .Subscribe(onStateInfo =>
                {
                    AnimatorStateInfo info = onStateInfo.StateInfo;
                    if (info.IsName("Attack.NormalAttackAnimation")||info.IsName("Attack.AccumulationAttackAnimation"))
                    {
                        _isInAttack.Value = true;
                    }
                }).AddTo(this);

            // 攻撃関係のステートマシンから出た
            trigger
                .OnStateExitAsObservable()
                .Subscribe(onStateInfo =>
                {
                    AnimatorStateInfo info = onStateInfo.StateInfo;
                    if (info.IsName("Attack.NormalAttackAnimation")||info.IsName("Attack.AccumulationAttackAnimation"))
                    {
                        _isInAttack.Value = false;
                        OnAttackEndEvent();
                    }
                }).AddTo(this);
        }
        #endregion
        

        #region 攻撃のメソッド
        // 攻撃モーションに合わせて当たり判定をON/OFFする
        public void OnNormalAttackEvent()
        {
            _attackCollider1.enabled = true;
            _playercore.ChangeAttackPower(1);
        }

        public void OnChargeAttackEvent()
        {
            _attackCollider2.enabled = true;
            _playercore.ChangeAttackPower(2);
        }

        //当たり判定を消す
        public void OnAttackEndEvent()
        {
            _attackCollider1.enabled = false;
            _attackCollider2.enabled = false;
            _playercore.ChangeAttackPower(0);
        }
        #endregion


        #region 攻撃判定用コライダの衝突判定購読

        // 各種衝突判定を購読する
        private void SubscribeColliderEvent()
        {
            // OnTriggerEnter2DAsObservableをコンポーネントに対して呼び出すと、
            // そのコンポーネントの付与されたGameObjectに自動的に
            // 衝突検知用のコンポーネントがAddComponentされる
            _attackCollider1.OnTriggerEnter2DAsObservable()
                .Merge(_attackCollider2.OnTriggerEnter2DAsObservable())
                .Subscribe(x =>
                {
                    // 武器に当たった相手がダメージを与えられる相手であるか
                    if (!x.TryGetComponent<IApplyDamage>(out IApplyDamage attack)) return;
                    // 相手にダメージを与える

                    //todo:Playercoreから攻撃力。playerbuffからバフを受け取ってdamageクラスに入れる
                    
                    attack.Damaged(new  DamageToEnemy(1, 0));

                }).AddTo(this);
        }

        #endregion
    }
}