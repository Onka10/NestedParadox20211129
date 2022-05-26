using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Cards{
    public class DustDevil :MonoBehaviour, ICard
    {
        public bool CheckTrigger(){
            if(MonsterManager.I.MonsterCount  >= 1) return true;
            else return false;
        } 

        public string GetText(){
            return "フィールド上のモンスターを一体以上破壊して召喚出来る。破壊した数×10の攻撃力を得る";
        }
    }
}