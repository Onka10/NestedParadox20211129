using UniRx;
using UnityEngine;
using NestedParadox.Cards;

namespace NestedParadox.Players
{
    public class PlayerCardAction : MonoBehaviour
    {
        // private NestedParadox.Cards.CardManager _cardmanager;
        [SerializeField]NestedParadox.Cards.CardManager _cardmanager;
        private PlayerInput _playerinput;

        void Start()
        {
            _playerinput = GetComponent<PlayerInput>();
            // _cardmanager = GetComponent<NestedParadox.Cards.CardManager>();

            _playerinput.OnPlayCard
                // .Subscribe(_=> Debug.Log("召喚"))
                .Subscribe(_=> _cardmanager.Play())
                .AddTo(this);

            _playerinput.OnDrawCard
            // .Subscribe(_=> Debug.Log("ドロー"))
            .Subscribe(_=> _cardmanager.Draw())
            .AddTo(this);

            _playerinput.OnChangeHand
            // .Subscribe(t=> Debug.Log("ホイール"+t))
            .Subscribe(t=> _cardmanager.ChangeHand(t))
            .AddTo(this);
        }
    }
}
