using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NestedParadox.Players;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] GameObject _pauseUI;

    void Start()
    {
        _input.OnPause
        .Subscribe(_ => {
            bool pauseon;
            pauseon = _pauseUI.activeSelf == true ? false:true;
            _pauseUI.SetActive(pauseon);
        })
        .AddTo(this);
    }

}
