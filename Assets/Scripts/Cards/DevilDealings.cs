using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;

namespace NestedParadox.Cards
{
    public class DevilDealings : MonoBehaviour,ICard,IMagic
    {//悪魔の取引
        //定数ダメージ
        private int damageCost = 2;

        public bool CheckTrigger(){
            //HPがダメージより多いなら発動可能
            if(PlayerCore.I.Hp.Value > damageCost)    return true;
            else    return false;
            
        }

        public void Execution(){
            //HPを減らす
            PlayerCore.I.DirectDamaged(damageCost);

            //ドローエナジー付与
            PlayerCore.I.AddDrawEnergy(5);
        }
    }
}
