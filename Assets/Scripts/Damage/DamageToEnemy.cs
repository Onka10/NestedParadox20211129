using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : Damage
{
    
    public DamageToEnemy(int damageValue, int knockBackValue)
    {
        this.damageValue = damageValue;
        this.knockBackValue = knockBackValue;
    }
    
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
