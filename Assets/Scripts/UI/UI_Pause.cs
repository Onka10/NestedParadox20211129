using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NestedParadox.Players;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{
    //急ぎなのでコード汚い
    //ModelとViewが混在
    [SerializeField] NestedParadox.Players.PlayerInput _input;
    [SerializeField] GameObject _pauseUI;
    
    [SerializeField] Text up;
    [SerializeField] Text down;
    [SerializeField] PlayerCore _playerCore;

    enum UIPosition
    {
        Up,
        Down,
    }

    private readonly ReactiveProperty<UIPosition> _uiPos = new ReactiveProperty<UIPosition>();

    void Start()
    {
        _input.OnPause
        .Subscribe(_ => {
            if(_pauseUI.activeSelf == false)  OpenPause();
            else ClosePause();
        })
        .AddTo(this);

        //UIの見た目
        _uiPos
        .Subscribe(p => {
            if(p == UIPosition.Up){
                up.enabled = true;
                down.enabled = false;
            }else{
                up.enabled = false;
                down.enabled = true;        
            }
        })
        .AddTo(this);
    }

    private void ClosePause(){
        _pauseUI.SetActive(false);
        _playerCore.EndPause();
    }

    private void OpenPause(){
        _pauseUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current == null)    return;
        
        var uKey = current.upArrowKey;
        var dKey = current.downArrowKey;
        var enter = current.enterKey;


        if(_playerCore.PauseState.Value){
            if (uKey.wasPressedThisFrame || dKey.wasPressedThisFrame)
            {
                _uiPos.Value = _uiPos.Value == UIPosition.Up ? UIPosition.Down:UIPosition.Up;
            }else if(enter.wasPressedThisFrame)
            {
                if(_uiPos.Value == UIPosition.Up)  ClosePause();
                else if(_uiPos.Value == UIPosition.Down)    SceneManager.LoadScene("TitleScene");
            }
        }

    }
}
