using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void ToCreditSecene(){
        SceneManager.LoadScene("CreditScene");
    }

    public void ToGameScene(){
        SceneManager.LoadScene("OnkaloMasterScene");
    }
}
