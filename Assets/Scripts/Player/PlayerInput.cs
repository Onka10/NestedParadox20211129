using System;
using UniRx;
using UniRx.Triggers; // UpdateAsObservable()の呼び出しに必要
using UnityEngine;

namespace NestedParadox.Players
{
    public class PlayerInput : MonoBehaviour
    {
        //購読される変数
        public IObservable<Unit> OnNormalAttack => _normalAttackSubject;
        public IObservable<Unit> OnChargeAttack => _chargeAttackSubject;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _move;


        // イベント発行に利用するSubjectやReactiveProperty
        private readonly Subject<Unit> _normalAttackSubject = new Subject<Unit>();
        private readonly Subject<Unit> _chargeAttackSubject = new Subject<Unit>();
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();



        // 長押しだと判定するまでの時間
        private static readonly float LongPressSeconds = 0.25f;



        private void Start(){
            //AddToでOnDestroy時にDispose()されるように登録する
            _normalAttackSubject.AddTo(this);
            _chargeAttackSubject.AddTo(this);
            _jump.AddTo(this);
            _move.AddTo(this);

            //updateをobservableにする
            this.UpdateAsObservable()
            // Attackボタンの状態を取得 
            .Select(_ => Input.GetMouseButton(0))
            // 値が変動した場合のみ通過
            .DistinctUntilChanged()
            // 最後に状態が変動してからの経過時間を付与
            .TimeInterval()
            .Skip(1)
            .Subscribe(t =>
            {
                // 攻撃ボタンを押した瞬間のイベントは無視
                if (t.Value) return;

                // 攻撃ボタンを押してから離すまでの時間で判定
                if (t.Interval.TotalSeconds >= LongPressSeconds)
                {
                    _chargeAttackSubject.OnNext(Unit.Default);
                }
                else
                {
                    _normalAttackSubject.OnNext(Unit.Default);
                }
            }).AddTo(this);
        }

        void FixedUpdate(){
            // ジャンプボタン
            _jump.Value = Input.GetKeyDown("space");

            // 移動入力をベクトルに変換して反映
            // ReactiveProperty.SetValueAndForceNotifyを使うと強制的にメッセージ発行できる
            _move.SetValueAndForceNotify(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
