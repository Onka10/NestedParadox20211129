using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : Damage
{

    //ダメージ量の変更
    public void SetDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }

    //ノックバック量の変更
    public void SetKnockBackValue(int knockBackValue)
    {
        this.knockBackValue = knockBackValue;
    }   
}
