using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Cards
{
    public class LastSword : MonoBehaviour,ICard,IMagic
    {//最後の剣
        public bool CheckTrigger(){
            if(MonsterManager.I.MonsterCount  >= 1) return true;
            else return false;
        }

        public void Execution(){
            Debug.Log("最後の剣！！！");
            var atk = MonsterManager.I.MonsterCount;
            //todo 必要に応じて倍率をかける。今はそのまま
            PlayerBuff.I.EnhanceATK(atk);
            PlayerEffectManager.I.EffectPlay(4);
            SoundManager.Instance.PlaySE(SESoundData.SE.LastSword);
            
            //FIXME呼び出し元の関係で非同期処理が出来ない
            //await EffetcTime();
            
        }

        // async UniTask EffetcTime(){
        //     await UniTask.Delay(1000);
        //     PlayerBuff.I.EnhanceATK(0);
        // }

        public string GetText(){
            return "フィールド上のモンスターの数だけ攻撃力を上げる";
        }
    }
}
