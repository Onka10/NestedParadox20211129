using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using NestedParadox.Players;
using System.Threading;

public class ShieldAttack : BossCommand
{
    //?V?[???h
    [SerializeField] GameObject omniShield;
    [SerializeField] GameObject shield_back;
    [SerializeField] GameObject[] originalShield;
    Rigidbody2D shieldRb;
    [SerializeField] Collider2D shieldColl;
    [SerializeField] Animator animator;    

    //?V?[???h???????????????????t???O
    private bool isGrounded;

    //?U???p?????[?^
    [SerializeField] Vector3 upForce;
    [SerializeField] Vector3 downForce;
    [SerializeField] int downTime;
    [SerializeField] float returnLerpRate;

    private Transform playerPos;    

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        isGrounded = false;
        shieldColl.OnTriggerEnter2DAsObservable().Subscribe(other =>
        {
            isGrounded = true;
            if(other.TryGetComponent<PlayerCore>(out PlayerCore player))
            {
                player.Damaged(new DamageToPlayer(attackPower, 0));
            }
        });        
    }

    public override async UniTask Execute(CancellationToken token)
    {
        await base.Execute(token);
        omniShield.SetActive(true);
        foreach (GameObject shield in originalShield)
        {
            shield.SetActive(false);
        }
        shield_back.transform.localPosition = new Vector3(0, 0, 1);
        Rigidbody2D shieldRb = omniShield.GetComponent<Rigidbody2D>();
        animator.SetTrigger("ShieldAttackTrigger");
        await UniTask.Delay(300, cancellationToken: token);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("ShieldAttack1"))
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);
            Debug.Log("攻撃1中");
            if (omniShield.transform.position.y > 20)
            {
                shieldRb.velocity = Vector3.zero;
                continue;
            }
            shieldRb.AddForce(upForce);            
        }
        shieldRb.velocity = new Vector3(0, 0, 0);
        await UniTask.Delay(downTime, cancellationToken: token);
        shieldRb.position = new Vector3(playerPos.position.x, omniShield.transform.position.y, omniShield.transform.position.z);
        while(animator.GetCurrentAnimatorStateInfo(0).IsName("ShieldAttack2"))
        {
            if(isGrounded)
            {
                shieldRb.velocity = Vector3.zero;
                await UniTask.Yield(cancellationToken: token);
                continue;
            }
            Debug.Log("攻撃２中");
            shieldRb.AddForce(downForce);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);            
        }

        shieldRb.velocity = new Vector3(0, 0, 0);
        while(transform.InverseTransformPoint(shieldRb.position).magnitude > 0.1f)
        {
            //Debug.Log("攻撃3中");
            shieldRb.position = Vector3.Lerp(shieldRb.position, transform.position, returnLerpRate);
            await UniTask.Yield(cancellationToken: token);
        }
        isGrounded = false;
        foreach (GameObject shield in originalShield)
        {
            shield.SetActive(true);
        }
        omniShield.SetActive(false);
        animator.SetTrigger("IdleTrigger");
    }


}
