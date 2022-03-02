using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UI_footer : MonoBehaviour
{
    [SerializeField] NestedParadox.Cards.CardManager _cardmaganeger;

    [SerializeField] Text decktext;
    [SerializeField] Text graveyardtext;
    public List<GameObject> UI_handselect = new List<GameObject>(3);
    public List<GameObject> UI_hand = new List<GameObject>(3);


    void Start(){
        Init();

        _cardmaganeger.Deck
        .ObserveCountChanged()
        .Subscribe(count => UpdateDeckCount(count))
        .AddTo(this);

        _cardmaganeger.Hand
        .ObserveRemove()
        .Subscribe(_ => UpdateHand())
        .AddTo(this);

        _cardmaganeger.GraveyardCount 
        .Subscribe(x=>UpdateGraveyardCount(x))
        .AddTo(this);

        _cardmaganeger.NowHand
        .Subscribe(x => UpdateSelecthand(x))
        .AddTo(this);
    }

    private void Init(){
        //Listの購読がSubscribeをしても初回に動作してくれない事への暫定的対応
        decktext.text = 7.ToString();
    }

    private void UpdateDeckCount(int x){
        decktext.text = x.ToString();
    }

    private void UpdateGraveyardCount(int x){
        graveyardtext.text = x.ToString();
    }

    private void UpdateSelecthand(int x){
        for(int a=0;a<3;a++)  UI_handselect[a].SetActive(false);
        UI_handselect[x].SetActive(true);
    }

    private void UpdateHand(){
        //一度消す。将来的にはアニメーションの処理が入る
        for(int a=0;a<3;a++)  UI_hand[a].SetActive(false);
        //手札枚数分だけつける
        for(int z=0;z<_cardmaganeger.Hand.Count;z++)    UI_hand[z].SetActive(true);
    }
}
