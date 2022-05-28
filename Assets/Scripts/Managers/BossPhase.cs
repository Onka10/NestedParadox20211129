using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NestedParadox.Stages;
using NestedParadox.Monsters;
using UniRx;

namespace NestedParadox.Managers
{
    public class BossPhase : PhaseBase
{
        [SerializeField] GameObject bossPrefab;
        [SerializeField] StageManager stageManager;
        [SerializeField] MonsterManager monsterManager;
        [SerializeField] UI_BossHP ui_bossHp;
        [SerializeField] Animator fadeAnimator;
        private bool bossIsDeath;
        public override async UniTask Execute()
        {
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Boss);
            player.transform.position = new Vector3(10, 0, 0);            
            bossIsDeath = false; //パラメータ初期化
            //BOSSステージへの切り替え
            mainCamera.ChangeToBossCamera();
            stageManager.DeleteCurrentStage();
            int[] stageIndexList = { 4 };
            stageManager.RandomGenerateStage(stageIndexList);
            EnemyBase boss = Instantiate(bossPrefab).GetComponent<EnemyBase>();
            boss.IsDeath.Subscribe(_ => bossIsDeath = true).AddTo(boss.gameObject);
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
            ui_bossHp.Activate(boss);
            await UniTask.WaitUntil(() => !fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"));
            Debug.Log("フェードインが終わりました");
            monsterManager.ActivateCurrentMonster();
            await UniTask.WaitUntil(() => bossIsDeath, cancellationToken: this.GetCancellationTokenOnDestroy());            
            //ボスが死んだらモンスターを攻撃しない状態にする。
            monsterManager.InActivateCurrentMonster();
            Debug.Log("ボスを倒しました");
            boss.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            await UniTask.Delay(2000, cancellationToken: this.GetCancellationTokenOnDestroy());
            fadeAnimator.SetTrigger("FadeOutTrigger");
            await UniTask.Delay(2000, cancellationToken: this.GetCancellationTokenOnDestroy());          
            next = null;
        }        
    }
}