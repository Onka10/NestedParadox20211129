using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
// using UniRxSampleGame.Damages;
using UnityEngine;

namespace NestedParadox.Players
{
    // プレイヤの攻撃処理の管理コンポーネント
    public class PlayerAttack : MonoBehaviour
    {
        // 現在の攻撃力
        [SerializeField] private int _attackPower = 1;
        // private IInputEventProvider _inputEventProvider;
        private PlayerAnimation _playerAnimation;
        private PlayerMove _playerMove;



        private readonly ReactiveProperty<bool> _isInAttack = new ReactiveProperty<bool>(false);

        // ヒエラルキー上で子要素として存在する攻撃判定用コライダ
        [SerializeField] private Collider2D _attackCollider1;
        [SerializeField] private Collider2D _attackCollider2;

        private void Start()
        {
            _isInAttack.AddTo(this);

            // _inputEventProvider = GetComponent<IInputEventProvider>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerMove = GetComponent<PlayerMove>();

            // 操作イベントの購読
            // SubscribeInputEvent();

            // アニメーションイベントの購読
            // SubscribeAnimationEvent();

            // 衝突イベントの購読
            // SubscribeColliderEvent();

            // 攻撃中は移動不可フラグを立てる
            _isInAttack
                .Subscribe(x => _playerMove.BlockMove(x))
                .AddTo(this);

            // OnAttackEndEvent();
        }




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
                    // if (!x.TryGetComponent<IDamageApplicable>(out var d)) return;

                    // 斜め上方向にふっとばすベクトルを計算
                    var direction =
                        ((x.transform.position - transform.position).normalized + Vector3.up)
                        .normalized;
                    // 相手にダメージを与える
                    // d.ApplyDamage(new Damage(_attackPower, direction));
                }).AddTo(this);
        }

    }
}
