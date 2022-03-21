using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage
{
    //ダメージ量
    [SerializeField] protected int damageValue;
    public int DamageValue => damageValue;

    //ノックバック量
    [SerializeField] protected int knockBackValue;
    public int KnockBackValue => knockBackValue;

}
