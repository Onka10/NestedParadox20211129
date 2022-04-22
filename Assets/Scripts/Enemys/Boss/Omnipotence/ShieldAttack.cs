using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ShieldAttack : BossCommand
{
    //シールド
    [SerializeField] GameObject omniShield;
    Rigidbody2D shieldRb;
    Collider2D shieldColl;
    [SerializeField] Animator animator;

    //シールドが下に落下した時のフラグ
    private bool isGrounded;

    //攻撃パラメータ
    [SerializeField] Vector3 upForce;
    [SerializeField] Vector3 downForce;
    [SerializeField] int downTime;

    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        isGrounded = false;
        shieldColl.OnCollisionEnter2DAsObservable().Subscribe(_ =>
        {
            isGrounded = true;
        });
    }

    public override async UniTask Execute()
    {
        await base.Execute();
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("ShieldAttack1"))
        {
            omniShield.GetComponent<Rigidbody2D>().AddForce(upForce);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        await UniTask.Delay(downTime);
        omniShield.transform.position = new Vector3(omniShield.transform.position.x, playerPos.position.y, omniShield.transform.position.z);
        while(animator.GetCurrentAnimatorStateInfo(0).IsName("ShieldAttack2"))
        {
            omniShield.GetComponent<Rigidbody2D>().AddForce(downForce);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
    }


}
