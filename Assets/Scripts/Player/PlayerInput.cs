using System;
using System.Threading;
using UniRx;
using UniRx.Triggers; // UpdateAsObservable()の呼び出しに必要
using UnityEngine;
using UnityEngine.InputSystem;



namespace NestedParadox.Players
{
    public class PlayerInput : MonoBehaviour
    {
        //購読される変数
        public IObservable<Unit> OnNormalAttack => _normalAttackSubject;
        public IObservable<Unit> OnChargeAttack => _chargeAttackSubject;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _move;
        public IObservable<Unit> OnPlayCard => _playcardsubject;
        public IObservable<Unit> OnDrawCard => _drawcardsubject;
        public IObservable<Unit> OnChangeHandR => _changehandRsubject;
        public IObservable<Unit> OnChangeHandL => _changehandLsubject;


        // イベント発行に利用するSubjectやReactiveProperty
        private readonly Subject<Unit> _normalAttackSubject = new Subject<Unit>();
        private readonly Subject<Unit> _chargeAttackSubject = new Subject<Unit>();
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
        private readonly Subject<Unit> _playcardsubject = new Subject<Unit>();
        private readonly Subject<Unit> _drawcardsubject = new Subject<Unit>();
        private readonly Subject<Unit> _changehandRsubject = new Subject<Unit>();
        private readonly Subject<Unit> _changehandLsubject = new Subject<Unit>();         


        //赤さんのカメラ
        public IReadOnlyReactiveProperty<int> CurrentDirection => currentDirection.Select(x => x.x < 0 ? 1 : -1).ToReactiveProperty<int>();
        private ReactiveProperty<Vector3> currentDirection = new ReactiveProperty<Vector3>();
        public Transform MyTransform { get { return myTransform; } }
        private Transform myTransform;

        private void Start(){
            //AddToでOnDestroy時にDispose()されるように登録する
            _normalAttackSubject.AddTo(this);
            _chargeAttackSubject.AddTo(this);
            _jump.AddTo(this);
            _move.AddTo(this);
            _playcardsubject.AddTo(this);
            _drawcardsubject.AddTo(this);
            _changehandRsubject.AddTo(this);
            _changehandLsubject.AddTo(this);

            //赤さんのカメラ
            myTransform = transform;
            currentDirection.Value = myTransform.localScale;
            currentDirection.AddTo(this);

        }

        void FixedUpdate(){
            // 移動入力をベクトルに変換して反映
            // ReactiveProperty.SetValueAndForceNotifyを使うと強制的にメッセージ発行できる
            _move.SetValueAndForceNotify(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }

        public void OnAttack(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed){//タメ攻撃
                _chargeAttackSubject.OnNext(Unit.Default);
            }else if(context.phase == InputActionPhase.Canceled){//通常攻撃
                _normalAttackSubject.OnNext(Unit.Default);
            }
        }
        public void CardRight(InputAction.CallbackContext context){           
            if(context.phase == InputActionPhase.Performed){
                _changehandRsubject.OnNext(Unit.Default);
            }
        }

        public void CardLeft(InputAction.CallbackContext context){
            if(context.phase == InputActionPhase.Performed){
                _changehandLsubject.OnNext(Unit.Default);
            }
        }

        public void OnJump(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Started){
                _jump.Value = true;
            }else if(context.phase == InputActionPhase.Canceled){
                _jump.Value = false;
            }
        }

        public void OnPlay(InputAction.CallbackContext context){
            if(context.phase == InputActionPhase.Started){
                _playcardsubject.OnNext(Unit.Default);
            }
        }

        public void OnDraw(InputAction.CallbackContext context){
            if(context.phase == InputActionPhase.Started){
                _drawcardsubject.OnNext(Unit.Default);
            }
        }
    }
}
