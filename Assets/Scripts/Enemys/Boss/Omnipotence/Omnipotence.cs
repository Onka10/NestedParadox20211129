using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Omnipotence : EnemyBase
{
   //?A?^?b?N?????N???X
    [SerializeField] private OmnipotenceAttack attack;

    public override async void Damaged(Damage damage)
    {
        await UniTask.Yield();
    }
}
