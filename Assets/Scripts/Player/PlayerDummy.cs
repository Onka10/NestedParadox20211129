using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Players;
using UniRx;

namespace NestedParadox.Players{
    public class PlayerDummy : MonoBehaviour
    {
        [SerializeField] PlayerInput _playerinput;

        void Start(){
            _playerinput.OnDebug
            .Subscribe(_ => PlayerEffectManager.I.EffectPlay(1))
            .AddTo(this);
        }

        //メモゾーン


    }
}
