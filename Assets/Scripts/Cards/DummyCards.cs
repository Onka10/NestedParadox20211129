using UnityEngine;

public class DummyCards : MonoBehaviour,ICard
{
        public bool CheckTrigger(){
            return true;
        }

        public string GetText(){
            return "ダミー";
        }
}
