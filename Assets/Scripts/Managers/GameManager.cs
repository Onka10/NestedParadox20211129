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
        [SerializeField] StageManager stageManager;        
        [SerializeField] GameObject stageEnd;
        [SerializeField] PlayerCore playerCore;
        private int stageClearCount;

        void Start()
        {

            //マネージャの初期化
            //UIの初期化
            stageClearCount = 0;
            playerCore.transform.position = Vector3.zero;
            stageManager.Construct();
            stageEnd.OnTriggerEnter2DAsObservable().Where(other => other.CompareTag("MainCharacter"))
                    .Subscribe(_ => OnReachStageEnd())
                    .AddTo(this);                        
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnReachStageEnd()
        {
            stageManager.DeleteCurrentStage();
            stageManager.RandomGenerateStage();
            playerCore.transform.position = Vector3.zero;
            stageClearCount++;
            if(stageClearCount >= 4)
            {

            }
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
