using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;

namespace NestedParadox.Cards
{
    public class LastSword : MonoBehaviour,ICard,IMagic
    {//最後の剣

        [SerializeField] MonsterManager _monstermanager;
        [SerializeField] PlayerBuff _playerbuff;
        public bool CheckTrigger(){
            //多分無し
            return true;
        }

        public void Execution(){
            // var atk = _monstermanager.MonsterCount.Value
            //todo 必要に応じて倍率をかける。今はそのまま
            // _playerbuff.EnhanceATK(atk);

            Debug.Log("最後の剣！！！");
        }
    }
}
