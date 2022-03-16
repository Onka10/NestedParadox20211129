using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;

namespace NestedParadox.Cards
{
    public class DevilDealings : MonoBehaviour,IMagic
    {//悪魔の取引

        [SerializeField] MonsterManager monsterManager;
        [SerializeField] PlayerCore playerCore;

        //定数ダメージ
        private int damagecost = 1;

        void Start(){
        }

        public bool CheckCondition(){
            //HPがダメージより多いなら発動可能
            if(playerCore.PlayerHP.Value > damagecost){
                ExecutionMagic();
                return true;
            }else{
                return false;
            }
        }

        public void ExecutionMagic(){
            //HPを減らす
            playerCore.DirectDamaged(damagecost);

            //ダストデビルを召喚。ダストデビルのカードidは0
            monsterManager.Summon(0);
        }
    }
}
