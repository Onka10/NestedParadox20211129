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
        [SerializeField] UI_BossHP ui_bossHp;
        private bool bossIsDeath;
        public override async UniTask Execute()
        {
            player.transform.position = new Vector3(10, 0, 0);
            bossIsDeath = false; //パラメータ初期化
            //BOSSステージへの切り替え
            mainCamera.ChangeToBossCamera();
            stageManager.DeleteCurrentStage();
            int[] stageIndexList = { 4 };
            stageManager.RandomGenerateStage(stageIndexList);
            EnemyBase boss = Instantiate(bossPrefab).GetComponent<EnemyBase>();
            boss.IsDeath.Subscribe(_ => bossIsDeath = true).AddTo(boss.gameObject);
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());//bossのStart関数待ち
            ui_bossHp.Activate(boss);
            await UniTask.WaitUntil(() => bossIsDeath, cancellationToken: this.GetCancellationTokenOnDestroy());
        }        
    }
}