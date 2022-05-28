using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MissileShot : BossCommand
{
    [SerializeField] GameObject missilePrefab;    
    [SerializeField] Animator animator;
    [SerializeField] float areaLimit_left;
    [SerializeField] float areaLimit_right;
    [SerializeField] float shotForce;

    public override async UniTask Execute(CancellationToken token)
    {
        await base.Execute(token);                                
        animator.SetTrigger("MissileShotTrigger");
        await UniTask.Delay(300);
        //攻撃前
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("MissileShot1"))
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
            Debug.Log("攻撃前");           
        }

        //攻撃中
        Debug.Log("攻撃中");
        List<Vector3> destinations = new List<Vector3>();
        //ミサイル生成
        for (int i = 0; i < 2; i++)
        {
            float random = Random.Range(areaLimit_left, areaLimit_right);
            Vector3 destination = new Vector3(Random.Range(areaLimit_left, areaLimit_right), 0, 0);
            GameObject missile_clone = Instantiate(missilePrefab);
            OmniMissile omniMissile = missile_clone.GetComponent<OmniMissile>();            
            missile_clone.transform.position = new Vector3(random, 20, 10+ 5*i);
            missile_clone.transform.localScale = new Vector2(1.2f, 1.2f);
            omniMissile.SetAttackPower(attackPower);
            omniMissile.Shot(destination, shotForce, false);            
        }
        await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("MissileShot2"), cancellationToken: token);

        //攻撃後
        Debug.Log("攻撃後");
        await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("MissileShot3"), cancellationToken: token);
        
    }
}
