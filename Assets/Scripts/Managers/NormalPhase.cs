using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Stages;

namespace NestedParadox.Managers
{
    public class NormalPhase : PhaseBase
    {
        [SerializeField] private int firstEnemyCount;//最初の敵の数
        private int currentEnemyCount;//現在のエネミーの数
        [SerializeField] private EnemyBase[] enemys;
        [SerializeField] private GameObject stageEnd;
        [SerializeField] StageManager stageManager;
        

        //ノーマルフェイズ実行
        public async override UniTask Execute()
        {
            currentEnemyCount = firstEnemyCount;
            foreach(EnemyBase enemy in enemys)
            {
                enemy.IsDeath.Subscribe(_ =>
                {
                    currentEnemyCount -= 1;
                })
                .AddTo(this);
            }


            await UniTask.WaitUntil(() => currentEnemyCount <= 0, cancellationToken: this.GetCancellationTokenOnDestroy());
            stageEnd.SetActive(true);
            stageEnd.GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                .Subscribe(_ => OnReachStageEnd())
                .AddTo(this);


        }

        private void OnReachStageEnd()
        {
            stageManager.DeleteCurrentStage();
            stageManager.RandomGenerateStage();
            player.transform.position = Vector3.zero;
            stageEnd.SetActive(false);
        }


    }
}