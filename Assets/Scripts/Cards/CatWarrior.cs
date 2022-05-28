using UnityEngine;

namespace NestedParadox.Cards{
    public class CatWarrior :MonoBehaviour, ICard
    {
        public bool CheckTrigger(){
            return true;
        }
        public string GetText(){
            return "プレイヤーのHPが10減るたびこのモンスターの攻撃力は10増える";
        }
    }
}
