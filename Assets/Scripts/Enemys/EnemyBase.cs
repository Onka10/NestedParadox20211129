using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public abstract class EnemyBase : MonoBehaviour
{
    protected int hp;
    protected ReactiveProperty<int> hp_r = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Hp => hp_r;
    protected int attackPower;
    protected ReactiveProperty<EnemyState> state = new ReactiveProperty<EnemyState>();
    public IReadOnlyReactiveProperty<EnemyState> State => state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        
    }

    public abstract void Damaged(Damage damage);
}

public enum EnemyState
{
    Idle,
    Attack
}
