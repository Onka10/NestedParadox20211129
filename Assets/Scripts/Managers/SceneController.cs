using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public void Clear()
    {
        // イベントに登録
        SceneManager.sceneLoaded += ResultClearLoaded;

        // シーン切り替え
        SceneManager.LoadScene("ResultScene");
    }

    public void GameOver()
    {
        // イベントに登録
        SceneManager.sceneLoaded += ResultGameOverLoaded;

        // シーン切り替え
        SceneManager.LoadScene("ResultScene");
    }

    private void ResultClearLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var resultManager= ResultManager.I;
        
        // データを渡す処理
        resultManager._result = ResultManager.Result.Clear;

        // イベントから削除
        SceneManager.sceneLoaded -= ResultClearLoaded;
    }

    private void ResultGameOverLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var resultManager= ResultManager.I;
        
        // データを渡す処理
        resultManager._result = ResultManager.Result.GameOver;

        // イベントから削除
        SceneManager.sceneLoaded -= ResultGameOverLoaded;
    }
}
