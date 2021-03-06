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
        [SerializeReference] SceneController sceneController;
        private PhaseBase phase;
        

        void Start()
        {
            phase = GetComponent<NormalPhase>();
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
            await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy()); //各オブジェクトの初期化待ち
            while(phase != null)
            {
                await phase.Execute();
                phase = phase.next;
            }
            sceneController.Clear();
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
        public void LoadToResultScene()
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
