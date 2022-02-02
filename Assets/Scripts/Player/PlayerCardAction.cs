using UniRx;
using UnityEngine;
using NestedParadox.Cards;

namespace NestedParadox.Players
{
    public class PlayerCardAction : MonoBehaviour
    {
        private NestedParadox.Cards.CardManager _cardmanager;
        private TempCharacter _playerinput;



        void Start()
        {
            _playerinput = GetComponent<TempCharacter>();
            _cardmanager = GetComponent<NestedParadox.Cards.CardManager>();

            _playerinput.OnPlayCard.Subscribe(OnPlayCard => Debug.Log(OnPlayCard));
        }


        
        private void SubscribeInputEvent()
        {
                // _playerinput.OnPlayCard
                // // 接地中なら攻撃ができる
                // // .Where(st => _playerinput.OnPlayCard.Value)
                // .Subscribe(st => Debug.Log(st))
                // .AddTo(this);

        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
