using System;
using UniRx;
using UniRx.Triggers; // UpdateAsObservable()の呼び出しに必要
using UnityEngine;

namespace NestedParadox.Players
{
    public class TempCharacter : MonoBehaviour
    {
        //購読される変数
        public IObservable<Unit> OnNormalAttack => _normalAttackSubject;
        public IObservable<Unit> OnChargeAttack => _chargeAttackSubject;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _move;
        public IReadOnlyReactiveProperty<string> OnPlayCard => _playcardsubject;
        


        // イベント発行に利用するSubjectやReactiveProperty
        private readonly Subject<Unit> _normalAttackSubject = new Subject<Unit>();
        private readonly Subject<Unit> _chargeAttackSubject = new Subject<Unit>();
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<string> _playcardsubject = new ReactiveProperty<string>("");


        //赤さんのカメラ
        public IReadOnlyReactiveProperty<int> CurrentDirection => currentDirection.Select(x => x.x < 0 ? 1 : -1).ToReactiveProperty<int>();
        private ReactiveProperty<Vector3> currentDirection = new ReactiveProperty<Vector3>();
        public Transform MyTransform { get { return myTransform; } }
        private Transform myTransform;


        // 長押しだと判定するまでの時間
        private static readonly float LongPressSeconds = 0.25f;



        private void Start(){
            //AddToでOnDestroy時にDispose()されるように登録する
            _normalAttackSubject.AddTo(this);
            _chargeAttackSubject.AddTo(this);
            _jump.AddTo(this);
            _move.AddTo(this);
            _playcardsubject.AddTo(this);

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

            //赤さんのカメラ
            myTransform = transform;
            currentDirection.Value = myTransform.localScale;

        }

        void FixedUpdate(){
            // ジャンプボタン
            _jump.Value = Input.GetKey("space");

            // 移動入力をベクトルに変換して反映
            // ReactiveProperty.SetValueAndForceNotifyを使うと強制的にメッセージ発行できる
            _move.SetValueAndForceNotify(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

            //召喚の処理。多分UniTaskに置き換える
            if(Input.GetKey(KeyCode.P)){
                _playcardsubject.Value = "しょうかん";
            }
        }
    }
}
