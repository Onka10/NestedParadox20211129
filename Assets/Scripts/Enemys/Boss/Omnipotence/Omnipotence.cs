using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omnipotence : MonoBehaviour
{
   //アタック管理クラス
    [SerializeField] private OmnipotenceAttack attack;

    public void Attack()
    {
        attack.Attack();
    }
}
