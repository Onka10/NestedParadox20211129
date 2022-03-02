using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NestedParadox.Players{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int HashSpeed = Animator.StringToHash("Speed");
        private static readonly int HashIsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int HashNormalAttack = Animator.StringToHash("NormalAttack");
        private static readonly int HashChargeAttack = Animator.StringToHash("ChargeAttack");


        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private PlayerMove _playerMove;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerMove = GetComponent<PlayerMove>();
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
            // Debug.Log("攻撃アニメーションの再生");
        }

        // ため攻撃を試みる
        public void ChargeAttack()
        {
            _animator.SetTrigger(HashChargeAttack);
            // Debug.Log("ため攻撃アニメーションの再生");
        }
    }
}
