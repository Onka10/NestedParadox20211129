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
        .Subscribe(_ => _pauseUI.SetActive(true))
        .AddTo(this);
    }

}
