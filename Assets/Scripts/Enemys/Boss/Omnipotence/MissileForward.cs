using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class MissileForward : BossCommand
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileAnimPrefab;
    [SerializeField] GameObject[] originalMissiles;
    [SerializeField] Animator animator;

    //生成時のミサイルポジション
    [SerializeField] Vector3 missileRightPosition;
    [SerializeField] Vector3 missileLeftPosition;
    [SerializeField] Vector3 missileRightRotation;
    [SerializeField] Vector3 missileLeftRotation;

    //アニメーション後のミサイルポジション
    [SerializeField] Vector3 missileRightPosition_after;
    [SerializeField] Vector3 missileLeftPosition_after;
    [SerializeField] Vector3 missileRightRotation_after;
    [SerializeField] Vector3 missileLeftRotation_after;

    //各パラメータ
    [SerializeField] float shotForce;
    [SerializeField] int setMissileAnimTime;
    [SerializeField] int returnMissileDelayTime;
    [SerializeField] int setMissileDelayTime;

    public override async UniTask Execute()
    {
        await base.Execute();
        animator.SetTrigger("MissileForwardTrigger");
        await UniTask.Delay(setMissileDelayTime);

        //左右のミサイルを生成してセット
        OmniMissile missile_clone_right = Instantiate(missileAnimPrefab).GetComponent<OmniMissile>();
        missile_clone_right.transform.SetParent(transform);
        missile_clone_right.transform.localPosition = missileRightPosition;
        missile_clone_right.transform.rotation = Quaternion.Euler(missileRightRotation);
        OmniMissile missile_clone_left = Instantiate(missileAnimPrefab).GetComponent<OmniMissile>();
        missile_clone_left.transform.SetParent(transform);
        missile_clone_left.transform.localPosition = missileLeftPosition;
        missile_clone_left.transform.rotation = Quaternion.Euler(missileLeftRotation);
        await UniTask.Delay(500);
        //元のミサイルを一時的に削除
        foreach (GameObject originalMissile in originalMissiles)
        {
            originalMissile.SetActive(false);
        }
        
        await UniTask.Delay(200);

        //左右ミサイルのセットアニメーション
        missile_clone_right.GetComponent<Animator>().SetTrigger("SetMissileRightTrigger");        
        missile_clone_left.GetComponent<Animator>().SetTrigger("SetMissileLeftTrigger");
        await UniTask.Delay(setMissileAnimTime);        

        //もう一度ミサイルの生成
        OmniMissile missile_clone_right_after = Instantiate(missilePrefab).GetComponent<OmniMissile>();
        missile_clone_right_after.transform.SetParent(transform);
        missile_clone_right_after.transform.localPosition = missileRightPosition_after;
        missile_clone_right_after.transform.rotation = Quaternion.Euler(missileRightRotation_after);
        OmniMissile missile_clone_left_after = Instantiate(missilePrefab).GetComponent<OmniMissile>();
        missile_clone_left_after.transform.SetParent(transform);
        missile_clone_left_after.transform.localPosition = missileLeftPosition_after;
        missile_clone_left_after.transform.rotation = Quaternion.Euler(missileLeftRotation_after);
        await UniTask.Delay(500);
        //アニメミサイルを破壊
        Destroy(missile_clone_right.gameObject);
        Destroy(missile_clone_left.gameObject);

        //ミサイル発射                
        Vector3 leftDestination = missile_clone_left_after.transform.position + new Vector3(-10, 0, 0);
        missile_clone_left_after.Shot(leftDestination, shotForce, true);
        await UniTask.Delay(1000); //　左右のミサイルの発射タイミングをずらす
        Vector3 rightDestination = missile_clone_right_after.transform.position + new Vector3(-10, 0, 0);
        missile_clone_right_after.Shot(rightDestination, shotForce, true);

        //ミサイルを戻す。
        await UniTask.Delay(returnMissileDelayTime);
        animator.SetTrigger("ReturnMissileTrigger");

        await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("MissileShot3"));
        foreach (GameObject originalMissile in originalMissiles)
        {
            originalMissile.SetActive(true);
        }
    }
}
