using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NestedParadox.Stages;

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
        }        
    }
}