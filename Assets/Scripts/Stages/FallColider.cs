using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Stages{
    public class FallColider : MonoBehaviour
    {
        //落下判定のクラス
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TryGetComponentでnullチェック
        //落下即死ならゲームオブジェクトを削除
        if(collision.gameObject.TryGetComponent<IFallingIsDead>(out IFallingIsDead dead))
        {
            //落下死メソッドを実行
            dead.Dead();
        }else if(collision.gameObject.TryGetComponent<IFallingIsRespown>(out IFallingIsRespown respown)){
            //リスポーンメソッドを実行
            respown.Respown();
        }
    }
    }

}