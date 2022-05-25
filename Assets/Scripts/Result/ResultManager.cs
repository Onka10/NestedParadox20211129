using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultManager :Singleton<ResultManager>
{
    public GameObject _clear;
    public GameObject _gameOver;

    public enum Result
    {
        Clear,
        GameOver,
    }

    public Result _result;

    void Start(){
        if(_result==Result.Clear)    _clear.SetActive(true);
        else if(_result==Result.GameOver)   _gameOver.SetActive(true);
    }


    void Update()
    {
        var current = Keyboard.current;
        if (current == null)    return;

        var enter = current.enterKey;

        if(enter.wasPressedThisFrame)
        {
            SceneManager.LoadScene("TitleScene");
        }

    }
}
