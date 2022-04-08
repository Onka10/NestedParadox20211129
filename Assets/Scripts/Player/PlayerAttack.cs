using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
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

        // ヒエラルキー上で子要素として存在する攻撃判定用コライダ
        [SerializeField] private Collider2D _attackCollider1;
        [SerializeField] private Collider2D _attackCollider2;

        private int nockback=0;

        private Animator animator;
        private AnimatorStateInfo stateInfo;



        private void Start()
        {
            _playerinput = GetComponent<PlayerInput>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerMove = GetComponent<PlayerMove>();
            _playercore = GetComponent<PlayerCore>();

            animator = GetComponent<Animator>();

            // 操作イベントの購読
            SubscribeInputEvent();

            // 衝突イベントの購読
            SubscribeColliderEvent();

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
                .Subscribe(_ => NAttack().Forget())
                .AddTo(this);

            // 強攻撃イベント
            _playerinput.OnChargeAttack
                // 接地中なら攻撃ができる
                .Where(_ => _playerMove.IsGrounded.Value)
                .ThrottleFirst(TimeSpan.FromSeconds(1))//連打防止。タメ攻撃はあ1秒
                .Subscribe(_ => CAttack().Forget())
                .AddTo(this);
        }

        //本来は攻撃が当たったときにドローエナジーを増やして欲しいけど、今はこれで我慢
        private async UniTask NAttack(){
            //アニメーションの再生
            _playerAnimation.NormalAttack();
            //コライダーをON
            _attackCollider1.enabled = true;
            //移動不可を設定
            _playerMove.BlockMove(true);
            //ステータスまわりを変更
            nockback = 1;
            _playercore.ChangeAttackPower(1);
            _playercore.AddDrawEnergy(10);
            
            await UniTask.DelayFrame(1);
            await UniTask.WaitWhile(() => _playerAnimation.IsAttack.Value);
            OnAttackEndEvent();
        }

        private async UniTask CAttack(){
            //アニメーションの再生
            _playerAnimation.ChargeAttack();
            //コライダーをONに
            _attackCollider2.enabled = true;
            //移動不可を設定
            _playerMove.BlockMove(true);
            //ステータスまわりを変更
            nockback = 2;
            _playercore.ChangeAttackPower(2);
            _playercore.AddDrawEnergy(2);

            await UniTask.DelayFrame(1);
            await UniTask.WaitWhile(() => _playerAnimation.IsAttack.Value);
            OnAttackEndEvent();
        }
        #endregion 

        #region 攻撃のメソッド
        //色々消す
        public void OnAttackEndEvent()
        {
            _attackCollider1.enabled = false;
            _attackCollider2.enabled = false;
            nockback = 0;
            _playercore.ChangeAttackPower(0);
            //移動不可を解除
            _playerMove.BlockMove(false);
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

                    //攻撃の実数値を入力
                    int AttackActualValue;
                    AttackActualValue = PlayerCore.I.PlayerAttackPower.Value + PlayerBuff.I.EnhancedATK.Value;
                    
                    attack.Damaged(new  DamageToEnemy(AttackActualValue, nockback));

                }).AddTo(this);

            _attackCollider2.OnTriggerEnter2DAsObservable()
                .Merge(_attackCollider2.OnTriggerEnter2DAsObservable())
                .Subscribe(x =>
                {
                    // 武器に当たった相手がダメージを与えられる相手であるか
                    if (!x.TryGetComponent<IApplyDamage>(out IApplyDamage attack)) return;

                    //攻撃の実数値を入力
                    int AttackActualValue;
                    AttackActualValue = PlayerCore.I.PlayerAttackPower.Value + PlayerBuff.I.EnhancedATK.Value;
                    
                    attack.Damaged(new  DamageToEnemy(AttackActualValue, nockback));

            }).AddTo(this);
        }
        #endregion
    }
}