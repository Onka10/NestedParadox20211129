using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UI_footer : MonoBehaviour
{
    //暫定的なクラスのため、カード準備画面やカード種類の増加に合わせて拡張や作り直しになります

    // [SerializeField] NestedParadox.Cards.CardManager _cardmaganeger;

    [SerializeField] Text decktext;
    [SerializeField] Text graveyardtext;
    public List<GameObject> UI_handselect = new List<GameObject>(3);
    public List<GameObject> UI_hand = new List<GameObject>(3);

    public Sprite[] CardsIconImages = new Sprite[7]; 
    Sprite cardicon;

    NestedParadox.Cards.CardManager _cardmaganeger;


    void Start(){
        //カードマネージャのキャッシュ
        _cardmaganeger = NestedParadox.Cards.CardManager.I;

        Init();

        _cardmaganeger.Deck
        .ObserveCountChanged()
        .Subscribe(count => UpdateDeckCount(count))
        .AddTo(this);

        _cardmaganeger.Hand
        .ObserveAdd()//ドロー
        .Subscribe(_ => UpdateHand())
        .AddTo(this);

        _cardmaganeger.Hand
        .ObserveRemove()//召喚
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

        //手札の初期化
        UpdateHand();
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
        //一度消す。将来的にはアニメーションの処理が入る？
        for(int a=0;a<3;a++)  UI_hand[a].SetActive(false);
        
        for(int z=0;z<_cardmaganeger.Hand.Count;z++){
            UI_hand[z].SetActive(true);
            CardCheck(z);
            UI_hand[z].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = cardicon;
        }
    }

    private void CardCheck(int num){
        //手札numに対して適切なカードアイコンを設定します
        int id = _cardmaganeger.Hand[num];
        cardicon = CardsIconImages[id];
    }
}
