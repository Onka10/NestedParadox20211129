using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NestedParadox.Stages;
using UniRx;
using UniRx.Triggers;

namespace NestedParadox.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] StageManager stageManager;
        [SerializeField] TempCharacter player;
        [SerializeField] GameObject stageEnd;

        void Start()
        {
<<<<<<< HEAD
            //stageManager.Construct();
            /*stageEnd.OnTriggerEnter2DAsObservable().Where(other => other.CompareTag("MainCharacter"))
=======
            //マネージャの初期化
            //UIの初期化

            stageManager.Construct();
            stageEnd.OnTriggerEnter2DAsObservable().Where(other => other.CompareTag("MainCharacter"))
>>>>>>> 15e818ce3f80af0d3921cfdb2f5e92d64d73c80b
                    .Subscribe(_ => OnReachStageEnd())
                    .AddTo(this);
            */
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnReachStageEnd()
        {
            stageManager.DeleteCurrentStage();
            stageManager.RandomGenerateStage();
            player.transform.position = new Vector3(0, 0, 0);
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
