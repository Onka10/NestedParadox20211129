using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Stages;
using NestedParadox.Monsters;
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
        [SerializeField] private GameObject stageEndPrefab;
        [SerializeField] StageManager stageManager;
        [SerializeField] private float areaLimit_Left;
        [SerializeField] private float areaLimit_Right;
        [SerializeField] private Animator fadeAnimator;
        [SerializeField] private MonsterManager monsterManager;        
        

        //ノーマルフェイズ実行
        public async override UniTask Execute()
        {            
            isReached = false;
            mainCamera.ChangeToNormalCamera();            
            //stageClearの回数が３回までは繰り返す
            stageClearCount = 0;
            while(stageClearCount < 3)
            {                
                int[] stageIndexList = { 0, 1, 2, 3 };
                stageManager.RandomGenerateStage(stageIndexList);
                player.transform.position = Vector3.zero;                
                await UniTask.WaitUntil(() => !fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"), cancellationToken: this.GetCancellationTokenOnDestroy());
                monsterManager.ActivateCurrentMonster();
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
                        .AddTo(enemy.gameObject);
                    }                    
                }
                currentEnemyCount = enemyList.Count;

                //敵が全滅するまで待つ
                try
                {
                    await UniTask.WaitUntil(() => currentEnemyCount <= 0, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                catch(System.OperationCanceledException)
                {
                    throw;
                }
                
                GameObject stageEnd = Instantiate(stageEndPrefab);
                stageEnd.GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                    .Where(other => other.gameObject.CompareTag("MainCharacter"))
                    .Subscribe(_ => OnReachStageEnd())
                    .AddTo(stageEnd.gameObject);
                //扉に到着するまで待つ
                await UniTask.WaitUntil(() => isReached);
                stageClearCount++;
                //フェードアウト中はモンスターの動き停止
                monsterManager.InActivateCurrentMonster();
                fadeAnimator.SetTrigger("FadeOutTrigger");
                fadeAnimator.SetTrigger("FadeInTrigger");
                await UniTask.WaitUntil(() => !fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"), cancellationToken: this.GetCancellationTokenOnDestroy());                
                stageManager.DeleteCurrentStage();                
                isReached = false;
                enemyList.Clear();
                Destroy(stageEnd);
            }                
        }

        private void OnReachStageEnd()
        {            
            isReached = true;                                             
        }


    }
}