using UnityEngine;

namespace NestedParadox.Cards{
    public class SniperK :MonoBehaviour, ICard
    {
        public bool CheckTrigger(){
            return true;
        }

        public string GetText(){
            return "フィールドのモンスターが破壊される度に敵に強力な攻撃を放つ";
        }
    }
}
