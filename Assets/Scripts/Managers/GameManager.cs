using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NestedParadox.Stages;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Players;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Managers
{
    public class GameManager : MonoBehaviour
    {                      
        [SerializeField] PlayerCore playerCore;
        [SerializeField] NestedParadox.Cards.CardManager _onkaloCardManager;
        [SerializeField] NestedParadox.UI.UIManager _onkaloUIManager;
        private PhaseBase phase;
        

        void Start()
        {
            phase = GetComponent<BossPhase>();
            PhaseExecute();
            OnkaloInit();
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage);
        }

        private void OnkaloInit(){
            _onkaloCardManager.InitCard();
            _onkaloUIManager.InitUI();

        }

        private async void PhaseExecute()
        {
            while(phase != null)
            {
                await phase.Execute();
                phase = phase.next;
            }            
        }

        // Update is called once per frame
        void Update()
        {

        }

       
        

        public void LoadToTitleScene()
        {
            SceneManager.LoadScene("TitleScene");
        }
        public void LoadToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void LoadToSelectCardScene()
        {
            SceneManager.LoadScene("SelectCardScene");
        }
    }
}
