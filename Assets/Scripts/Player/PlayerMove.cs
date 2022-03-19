using UniRx;
using UnityEngine;

namespace NestedParadox.Players
{
    public class PlayerMove : MonoBehaviour
    {
        // 接地状態
        public IReadOnlyReactiveProperty<bool> IsGrounded => _isGrounded;
        // 落下中であるか
        // public bool IsFall => _rigidbody2D.velocity.y < 0;


        // 移動速度
        [SerializeField] private float _dashSpeed = 3;
        // ジャンプ速度
        [SerializeField] private float _jumpSpeed = 5.5f;
        // Raycast時のプレイヤの高さを補正する。最終的な画質に合わせる
        [SerializeField] private float _characterHeightOffset = 3f;
        // 接地判定に利用するレイヤ設定
        [SerializeField] LayerMask _groundMask;


        //地面の判定に使うレイ。接地の結果を受け取る
        private readonly RaycastHit2D[] _raycastHitResults = new RaycastHit2D[1];
        //地面の判定
        private readonly ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();
        private bool _isJumpReserved;
        //行動不能
        [SerializeField]private bool _isMoveBlock;


        //赤さんのカメラ
        public IReadOnlyReactiveProperty<int> CurrentDirection => currentDirection.Select(x => x.x < 0 ? 1 : -1).ToReactiveProperty<int>();
        private ReactiveProperty<Vector3> currentDirection = new ReactiveProperty<Vector3>();
        public Transform MyTransform { get { return myTransform; } }
        private Transform myTransform;


        //外部参照
        private PlayerCore _playerCore;
        private Rigidbody2D _rigidbody2D;
        private PlayerInput _playerinput;

        private void Start(){
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerCore = GetComponent<PlayerCore>();
            _playerinput = GetComponent<PlayerInput>();

            _isGrounded.AddTo(this);


            //赤さんのカメラ
            myTransform = transform;

            currentDirection.Value = myTransform.localScale;
            currentDirection.AddTo(this);
        }

        private void FixedUpdate()
        {
            // 接地判定処理
            CheckGrounded();

            // 上書きする移動速度の値
            var vel = Vector3.zero;

            //死亡判定をそのうちやる

            // 操作イベントから得られた移動量
            var moveVector = GetMoveVector();

            // 移動操作を反映する
            if (moveVector != Vector3.zero && !_isMoveBlock)
            {
                // Debug.Log("移動");
                vel = moveVector * _dashSpeed;
            }

            // ジャンプ
            if (_playerinput.IsJump.Value && _isGrounded.Value && !_isMoveBlock)
            {
                // Debug.Log("Jump");
                // Debug.LogError("ジャンプテスト");
                vel += Vector3.up * _jumpSpeed;
                _isJumpReserved = false;
            }

            // 重力落下分を維持する
            vel += new Vector3(0, _rigidbody2D.velocity.y, 0);

            // 速度を更新
            _rigidbody2D.velocity = vel;

            // if(_isGrounded.Value){
            //     Debug.Log("地面");
            // }

            myTransform = transform;
        }

        // 操作イベントの値から移動量を決定する
        private Vector3 GetMoveVector()
        {
            var x = _playerinput.MoveDirection.Value.x;
            if (x > 0.1f)
            {
                //ここで向き変更の通知をカメラに送る
                currentDirection.Value = Vector3.right;
                return Vector3.right;
            }
            else if (x < -0.1f)
            {
                currentDirection.Value = -Vector3.right;
                return -Vector3.right;
            }
            else
            {
                currentDirection.Value = Vector3.zero;
                return Vector3.zero;
            }
        }

        // 接地判定
        private void CheckGrounded()
        {
            // 地面に対してRaycastを飛ばして接地判定を行う
            var hitCount = Physics2D.CircleCastNonAlloc(
                origin: transform.position - new Vector3(0, _characterHeightOffset, 0),//2次元空間における円の原点となる点。
                radius: 0.1f,//円の半径
                direction: Vector2.down,//円の向きを表すベクトル。
                results: _raycastHitResults,//結果を受け取る配列
                distance: 0.1f,//円を投影する最大距離
                layerMask: _groundMask);//特定のレイヤーのコライダーのみを判別するためのフィルター

            _isGrounded.Value = hitCount != 0;
        }

        // 移動不可フラグ
        //本来はよそのスクリプトで使う予定だった
        public void BlockMove(bool isBlock)
        {
            _isMoveBlock = isBlock;
        }
    }
}
