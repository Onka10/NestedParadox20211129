using System;
using UniRx;
using UniRx.Triggers; // UpdateAsObservable()の呼び出しに必要
using UnityEngine;
using NestedParadox.Players;

namespace NestedParadox.Players
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMove _playerMove;
        void Awake(){
            _playerMove = GetComponent<PlayerMove>();
        }
        void Update(){
            if(Input.GetKey(KeyCode.D))
            {
                Debug.Log("移動!");
                _playerMove.Move(1);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                Debug.Log("移動!");
                _playerMove.Move(-1);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ジャンプ!");
                _playerMove.Jump();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("攻撃!");
            }
        }
    }
}
