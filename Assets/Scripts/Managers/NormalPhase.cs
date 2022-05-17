using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Stages;
using MainCamera;

namespace NestedParadox.Managers
{
    public class NormalPhase : PhaseBase
    {       
        private int currentEnemyCount;//現在のエネミーの数
        private int stageClearCount;
        private bool isReached; //扉に到着した時のフラグ            
        [SerializeField] private GameObject[] enemyPrefabs;        
        [SerializeField] private int[] enemyInstanceCount;//各エネミーの出現させる数
        [SerializeField] private GameObject stageEnd;
        [SerializeField] StageManager stageManager;
        [SerializeField] private float areaLimit_Left;
        [SerializeField] private float areaLimit_Right;
        

        //ノーマルフェイズ実行
        public async override UniTask Execute()
        {
            isReached = false;
            mainCamera.ChangeToNormalCamera();
            //stageClearの回数が３回までは繰り返す
            while(stageClearCount < 4)
            {
                stageManager.DeleteCurrentStage();
                stageManager.RandomGenerateStage();             
                List<EnemyBase> enemyList = new List<EnemyBase>();
                for(int i=0; i<enemyPrefabs.Length; i++)
                {
                    for(int j=0; j<enemyInstanceCount[i]; j++)
                    {
                        EnemyBase enemy = Instantiate(enemyPrefabs[i]).GetComponent<EnemyBase>();
                        enemy.transform.position = new Vector3(Random.Range(areaLimit_Left, areaLimit_Right), 2, 0);
                        enemyList.Add(enemy);
                        enemy.IsDeath.Subscribe(_ =>
                        {
                            currentEnemyCount -= 1;
                        })
                        .AddTo(enemy);
                    }                    
                }
                currentEnemyCount = enemyList.Count;

                //敵が全滅するまで待つ
                await UniTask.WaitUntil(() => currentEnemyCount <= 0, cancellationToken: this.GetCancellationTokenOnDestroy());
                stageEnd.SetActive(true);
                stageEnd.GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                    .Subscribe(_ => OnReachStageEnd())
                    .AddTo(this);
                //扉に到着するまで待つ
                await UniTask.WaitUntil(() => isReached);
                isReached = false;
                enemyList.Clear();
            }                
        }

        private void OnReachStageEnd()
        {
            isReached = true;
            stageClearCount++;
            stageManager.DeleteCurrentStage();
            stageManager.RandomGenerateStage();
            player.transform.position = Vector3.zero;
            stageEnd.SetActive(false);
        }


    }
}