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
        .Subscribe(_ => ClosePause())
        .AddTo(this);

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
        bool pauseon;
        pauseon = _pauseUI.activeSelf == true ? false:true;
        _pauseUI.SetActive(pauseon);
    }

    private void PauseAction(){
        if(_uiPos.Value == UIPosition.Up) ClosePause();
        // else SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current == null)    return;
        
        var uKey = current.upArrowKey;
        var dKey = current.downArrowKey;


        if(_playerCore.PauseState.Value){
            if (uKey.wasPressedThisFrame || dKey.wasPressedThisFrame)
            {
                _uiPos.Value = _uiPos.Value == UIPosition.Up ? UIPosition.Down:UIPosition.Up;
            }
            
        }

    }
}
