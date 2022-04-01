using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace NestedParadox.Players{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int HashSpeed = Animator.StringToHash("Speed");
        private static readonly int HashIsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int HashNormalAttack = Animator.StringToHash("NormalAttack");
        private static readonly int HashChargeAttack = Animator.StringToHash("ChargeAttack");
        private static readonly int HashDamaged = Animator.StringToHash("Damaged");



        public IReadOnlyReactiveProperty<bool> IsAttack => _isInAttack;
        private readonly ReactiveProperty<bool> _isInAttack = new ReactiveProperty<bool>(false);


        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private PlayerMove _playerMove;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerMove = GetComponent<PlayerMove>();

            // アニメーションイベントの購読
            SubscribeAnimationEvent();
        }

        void Update(){
            // 各種アニメーションの遷移
            //着地判定は常に行う
            _animator.SetBool(HashIsGrounded, _playerMove.IsGrounded.Value);

            var moveX = _rigidbody2D.velocity.x;
            var moveY = _rigidbody2D.velocity.y;

            //移動時、アニメーションの反転に使う
            if (Mathf.Abs(moveX) > 0.05f)
            {
                var scale = transform.localScale;
                scale.x = moveX > 0 ? -1 : 1;
                transform.localScale = scale;
            }

            _animator.SetFloat(HashSpeed, Mathf.Abs(moveX));
            
            // 攻撃は1F以内に遷移できなかった場合はリセットする
            // _animator.ResetTrigger(HashNormalAttack);
            // _animator.ResetTrigger(HashChargeAttack);
        }

        // 通常攻撃
        public void NormalAttack()
        {
            _animator.SetTrigger(HashNormalAttack);
        }

        // ため攻撃を試みる
        public void ChargeAttack()
        {
            _animator.SetTrigger(HashChargeAttack);
        }

        public void Damaged(){
            _animator.SetTrigger(HashDamaged);
        }


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
                    }
                }).AddTo(this);
        }
        #endregion
    }
}
