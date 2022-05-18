using UniRx;
using UnityEngine;
using NestedParadox.Cards;

namespace NestedParadox.Players
{
    public class PlayerCardAction : MonoBehaviour
    {
        // [SerializeField]NestedParadox.Cards.CardManager _cardmanager;
        private PlayerInput _playerinput;
        private PlayerCore _playercore;
        NestedParadox.Cards.CardManager _cardmanager;

        void Start()
        {
            //カードマネージャのキャッシュ
            _cardmanager = NestedParadox.Cards.CardManager.I;
            _playerinput = GetComponent<PlayerInput>();
            _playercore = GetComponent<PlayerCore>();

            _playerinput.OnPlayCard
            .Where(_ => !_playercore.PauseState.Value)
            .Subscribe(_=> _cardmanager.Play())
            .AddTo(this);

            _playerinput.OnDrawCard
            .Where(_ => _playercore.PlayerDrawEnergy.Value ==10 && !_playercore.PauseState.Value)//ドロエナジーの確認
            .Subscribe(_=>{
                _cardmanager.Draw();
                _playercore.ResetDrawEnergy();
            })
            .AddTo(this);

            _playerinput.OnChangeHandR
            .Where(_ => _cardmanager.Hand.Count != 0 && !_playercore.PauseState.Value)//手札があるときのみ実行
            .Subscribe(_=> _cardmanager.publicRotateHand(1))
            .AddTo(this);

            _playerinput.OnChangeHandL
            .Where(_ => _cardmanager.Hand.Count != 0 && !_playercore.PauseState.Value)//手札があるときのみ実行
            .Subscribe(_=> _cardmanager.publicRotateHand(-1))
            .AddTo(this);

            _playerinput.OnCardDelete
            .Where(_ => _cardmanager.Hand.Count == 3 && !_playercore.PauseState.Value)//手札が満タンのときのみ実行可能
            .Subscribe(_=> _cardmanager.DeleteAllCard())
            .AddTo(this);
        }
    }
}
