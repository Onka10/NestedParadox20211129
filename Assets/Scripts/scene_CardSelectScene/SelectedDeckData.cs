using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDeckData : MonoBehaviour
{

    // singletonを利用しています。
    // デッキのデータにアクセスするときは
    // SelectedDeckData.instance.deckDataにアクセスすると、int型配列を取得することができるはずです。
    public static SelectedDeckData instance;
    public int[] deckData;

    private void Awake()
    {
        instance = this;
    }

}
