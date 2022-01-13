using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NestedParadox.Players{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private PlayerMove _playerMove;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMove = GetComponent<PlayerMove>();
        }
    }
}
