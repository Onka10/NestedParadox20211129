using UnityEngine;

namespace NestedParadox.Cards{
    public class GuardKun :MonoBehaviour, ICard
    {
        public bool CheckTrigger(){
            return true;
        }

        public string GetText(){
            return "このモンスターの召喚数だけダメージを軽減する";
        }
    }
}
