using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NestedParadox.Stages;
using UniRx;

namespace NestedParadox.Managers
{
    public class BossPhase : PhaseBase
{
        [SerializeField] GameObject bossPrefab;
        [SerializeField] StageManager stageManager;
        private bool bossIsDeath;
        public override async UniTask Execute()
        {
            bossIsDeath = false; //パラメータ初期化
            //BOSSステージへの切り替え
            mainCamera.ChangeToBossCamera();
            stageManager.DeleteCurrentStage();
            int[] stageIndexList = { 4 };
            stageManager.RandomGenerateStage(stageIndexList);
            EnemyBase boss = Instantiate(bossPrefab).GetComponent<EnemyBase>();
            boss.IsDeath.Subscribe(_ => bossIsDeath = true).AddTo(boss.gameObject);
            await UniTask.WaitUntil(() => bossIsDeath, cancellationToken: this.GetCancellationTokenOnDestroy());
        }        
    }
}