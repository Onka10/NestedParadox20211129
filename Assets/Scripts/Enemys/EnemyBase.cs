using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int hp;
    protected ReactiveProperty<int> hp_r = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Hp => hp_r;
    [SerializeField] protected int attackPower;
    protected ReactiveProperty<EnemyState> state = new ReactiveProperty<EnemyState>();
    public IReadOnlyReactiveProperty<EnemyState> State => state;
    //死亡した時に発行するSubject
    protected readonly Subject<Unit> isDeath = new Subject<Unit>();
    public IObservable<Unit> IsDeath => isDeath;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp_r.Value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        
    }

    public abstract void Damaged(Damage damage);

    public virtual async void Death()
    {
        await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
        isDeath.OnNext(Unit.Default);
    }

}

public enum EnemyState
{
    Idle,
    Attack
}
