using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start(){
        // SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
    }

    // public void ToCreditSecene(){
    //     SceneManager.LoadScene("CreditScene");
    // }

    public void ToGameScene(){
        SceneManager.LoadScene("MasterScene");
        SoundManager.Instance.PlaySE(SESoundData.SE.Click_Decide);
        SoundManager.Instance.StopBGM();
    }
}
