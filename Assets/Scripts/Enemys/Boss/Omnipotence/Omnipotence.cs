using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omnipotence : MonoBehaviour
{
   //�A�^�b�N�Ǘ��N���X
    [SerializeField] private OmnipotenceAttack attack;

    public void Attack()
    {
        attack.Attack();
    }
}
