using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;

namespace NestedParadox.Cards
{
    public class LastSword : MonoBehaviour,ICard,IMagic
    {//最後の剣
        public bool CheckTrigger(){
            return true;
        }

        public void Execution(){
            var atk = MonsterManager.I.MonsterCount;
            //todo 必要に応じて倍率をかける。今はそのまま
            PlayerBuff.I.EnhanceATK(atk);
            Debug.Log("最後の剣！！！");

            StartCoroutine("Delay");

            PlayerBuff.I.EnhanceATK(0);
        }

        IEnumerator Delay()
        {
            //30秒待つ
            yield return new WaitForSeconds(30);
        }
    }
}
