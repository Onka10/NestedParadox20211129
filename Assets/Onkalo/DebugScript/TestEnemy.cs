using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class TestEnemy : MonoBehaviour,IApplyDamage
{
    public IReadOnlyReactiveProperty<int> Hp => _hp;

    private readonly ReactiveProperty<int> _hp = new ReactiveProperty<int>();

    public Collider2D colision;

    void Start(){

    }

    //引数のDamageは攻撃時に攻撃側が与えてください
    public void Damaged(Damage Damage){
        Debug.Log("攻撃成功"+Damage.DamageValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TryGetComponentでnullチェック
        //攻撃相手のオブジェクトにインターフェースが継承されていれば攻撃可能
        if(collision.gameObject.TryGetComponent<IApplyDamage>(out IApplyDamage attack))
        {
            //自分の攻撃力を引数として渡す
            //攻撃側は相手が何であろうと攻撃する事だけを考えれば良い
            attack.Damaged(new DamageToPlayer(10,0));
        } 
    }
}
