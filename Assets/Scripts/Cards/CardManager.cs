using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEditor;
using NestedParadox.Monsters;

namespace NestedParadox.Cards
{
    //エディタ拡張
    [CustomEditor (typeof(CardManager))]
    //複数選択有効
    [CanEditMultipleObjects]

    public class CardManager : Singleton<CardManager>
    {
        //購読される変数
        public IReactiveCollection<int> Deck => _deck;
        
        public IReactiveCollection<int> Hand => _hand;
        public IReadOnlyReactiveProperty<int> GraveyardCount => _graveyardcount;
        public IReadOnlyReactiveProperty<int> NowHand => _nowhand;

        //private
        private readonly ReactiveCollection<int> _deck = new ReactiveCollection<int>{0,1,1,1,2,3,3,4,5,6};//初期化済み
        private Queue<int> graveyard= new Queue <int>();
        private readonly ReactiveCollection<int> _hand = new ReactiveCollection<int>{0,0,0};
        private readonly ReactiveProperty<int> _graveyardcount = new ReactiveProperty<int>(0);
        private readonly IntReactiveProperty _nowhand = new IntReactiveProperty(0);

        CardPresenter _cardpresenter;

        void Start(){
            //キャッシュ
            _cardpresenter = CardPresenter.I;

            _deck.AddTo(this);
            _hand.AddTo(this);
            _graveyardcount.AddTo(this);
            _nowhand.AddTo(this);
            _nowhand.AddTo(this);

            InitDeck();
        }

        private void InitDeck(){
            //シャッフル
            for (int i = _deck.Count - 1; i > 0; i--){
                var j = UnityEngine.Random.Range(0, i+1); // ランダムで要素番号を１つ選ぶ（ランダム要素）
                var temp = _deck[i]; // 一番最後の要素を仮確保（temp）にいれる
                _deck[i] = _deck[j]; // ランダム要素を一番最後にいれる
                _deck[j] = temp; // 仮確保を元ランダム要素に上書き
            }

            //手札の用意
            for(int i= 0; i<3;i++){
                _hand[i] = _deck[i];
                _deck.RemoveAt(i);
            }
        }

        public void Draw(){
            if(_hand.Count !=3)//手札が満タンではない時に可能
            {
                Debug.Log("ドロー");
                //ドロー出来るかの確認
                if(_deck.Count==0)  DeckReload();

                //山札を引いて手札に加える
                _hand.Add(_deck[0]);
                _deck.RemoveAt(0);

                //UIの更新
                _graveyardcount.Value = graveyard.Count;

            }
        }

        public void Play(){
            if(_hand.Count >= 1)//手札が1枚以上の時
            {
                //TO DO処理を試みる
                if(_cardpresenter.Check(_hand[_nowhand.Value])){
                    //効果実行
                    if(!MonsterManager.I.CanSummon)  return;//trueなら召喚出来る
                    _cardpresenter.Execute(_hand[_nowhand.Value]);
                    Trash();
                }
            }
        }

        private void Trash(){//今の手札を捨てる
            //墓地へ捨てる
            graveyard.Enqueue(_hand[_nowhand.Value]);
            //UI更新
            _graveyardcount.Value = graveyard.Count;

            //手札を消す
            _hand.RemoveAt(_nowhand.Value);
            if(_hand.Count!=0 && _nowhand.Value!=0)   Rotatehand(-1);//手札が残っている&&手札の選択が最初じゃないなら、nowhand移動
        }

        public void publicRotateHand(int h){
            //Rotatehandの外部公開
            Rotatehand(h);
        }

        private void Rotatehand(int x){//引数は1か-1にしてね。手札のローテーションメソッド
            if(_nowhand.Value == 0 && x<0){
                _nowhand.Value = _hand.Count-1;//手札の最大枚数になる
                // Debug.Log("case1:now"+_nowhand.Value);
            }else if(_nowhand.Value == _hand.Count-1 && x>0){
                _nowhand.Value=0;
                // Debug.Log("case2:now"+_nowhand.Value);
            }else if(_hand.Count==0){
                _nowhand.Value=0;
                // Debug.Log("手札無し"+_nowhand.Value);
            }else{
                _nowhand.Value+=x;
                // Debug.Log("case3:now"+_nowhand.Value);
            }
            
            //クランプ
            _nowhand.Value = Mathf.Clamp(_nowhand.Value, 0, 2);
        }

        private void DeckReload(){//墓地を補充する
            //コピー
            int[] array = graveyard.ToArray();
            //消す
            graveyard.Clear();

            for(int i= 0; i<array.Length;i++){
                _deck.Add(array[i]);
            }
            //TO DO山札をシャッフルする必要があるかも
        }

        public void DeleteAllCard(){
            Trash();
            Trash();
            Trash();
        }

        public bool CheckTrigger(int hand){
            // return _cardpresenter.Check(_hand[hand]);
            return true;
        }


    //     #if UNITY_EDITOR
    //     // デバッグ確認用
    //     [CustomEditor(typeof(CardManager), true)]
    //     public class CollectionPreview : Editor
    //     {
    //         int additem = 0;

    //         public override void OnInspectorGUI()
    //         {
    //             // 親クラスを参照
    //             CardManager _drc = (CardManager)target;
    //             ReactiveCollection<int> _iCol = _drc._deck;

    //             int _count = _iCol.Count;
    //             if( _count == 0)
    //             {
    //                 EditorGUILayout.HelpBox("No exist items or No runtime.", MessageType.None);
    //                 return;
    //             }

    //             //リストの表示
    //             for( int i = 0; i < _count; i++)
    //             {
    //                 EditorGUILayout.BeginHorizontal();
    //                 _iCol[i] = EditorGUILayout.IntField("Deck number" + i.ToString(), (_iCol[i]));
    //                 if( GUILayout.Button("Delete"))
    //                 {
    //                     _iCol.RemoveAt(i);
    //                     EditorGUILayout.EndHorizontal();
    //                     return;
    //                 }
    //                 EditorGUILayout.EndHorizontal();
    //             }

    //             //ここから自作
    //             // nowhandを表示
    //             //private IntReactiveProperty _nowhand = new IntReactiveProperty(0); 原文
    //             IntReactiveProperty nowhand = _drc._nowhand;
    //             int n = nowhand.Value;

    //             EditorGUILayout.BeginHorizontal();
    //             EditorGUILayout.IntField("nowhand",n);
    //             EditorGUILayout.EndHorizontal();


    //             //Handの表示
    //             // 親クラスを参照
    //             ReactiveCollection<int> _ihand = _drc._hand;

    //             int _handcount = _ihand.Count;
    //             if( _handcount == 0)
    //             {
    //                 EditorGUILayout.HelpBox("No exist items or No runtime.", MessageType.None);
    //                 return;
    //             }
    //             for( int i = 0; i < _handcount; i++)
    //             {
    //                 EditorGUILayout.BeginHorizontal();
    //                 _ihand[i] = EditorGUILayout.IntField("Hand number" + i.ToString(), (_ihand[i]));
    //                 if( GUILayout.Button("Delete"))
    //                 {
    //                     _ihand.RemoveAt(i);
    //                     EditorGUILayout.EndHorizontal();
    //                     return;
    //                 }
    //                 EditorGUILayout.EndHorizontal();
    //             }
    //         }

    //     }
    // #endif

    }
}
