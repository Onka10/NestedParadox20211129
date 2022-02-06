using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Cards
{
    public class CardManager : MonoBehaviour
    {
        //山札
        [SerializeField] private List<int> deck = new List<int>();
        //墓地
        [SerializeField] private Queue<int> graveyard= new Queue <int>();
        //手札
        [SerializeField] private List<int> hand = new List<int>();

        [SerializeField]private int nowhand=0;

        void Start(){
            //初期化(仮)

            for(int i= 3; i<10;i++){
                //本来はシャッフルして入れる
                deck.Add(i);
            }

            for(int i= 0; i<3;i++){
                hand[i] = i;
            }
        }

        public void Draw(){
            if(hand.Count !=3)//手札が満タンではない&&ドローエナジーが溜まってる
            {
                DeckCheck();
                Debug.Log("ドロー");
                //山札を引いて手札に加える
                hand.Add(deck[0]);
                deck.RemoveAt(0);
            }
        }

        public void Play(){
            if(hand.Count >= 1)//手札が1枚以上の時
            {
                //処理を試みる
                //赤さんの処理を呼ぶ。成功したかどうかを返り血で貰う
                Debug.Log("召喚");

                //墓地へ捨てる
                graveyard.Enqueue(hand[nowhand]);

                //手札を消す
                hand.RemoveAt(nowhand);
            }
        }

        public void ChangeHand(float h){
            if(h < 0){
                if(nowhand>0)  nowhand--;
            }else{
                if(nowhand<3)   nowhand++;
            }
        }

        private void DeckCheck(){
            //山札が無ければ墓地を補充する
            if(deck.Count==0){
                //コピー
                int[] array = graveyard.ToArray();
                //消す
                graveyard.Clear();
                //シャッフルをする？要検討
                for(int i= 0; i<array.Length;i++){
                    deck.Add(array[i]);
                }
            }
        }
    }
}
