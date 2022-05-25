using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NestedParadox.Stages;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Players;

namespace NestedParadox.Managers
{
    public class GameManager : MonoBehaviour
    {                      
        [SerializeField] PlayerCore playerCore;
        private PhaseBase phase;
        

        void Start()
        {
            phase = GetComponent<BossPhase>();
            PhaseExecute();
        }

        private async void PhaseExecute()
        {
            while(phase != null)
            {
                await phase.Execute();
                //phase = phase.next;
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
