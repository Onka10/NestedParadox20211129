using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omnipotence : MonoBehaviour
{
   //?A?^?b?N?????N???X
    [SerializeField] private OmnipotenceAttack attack;

    public void Attack0()
    {
        attack.Execute(0);
    }

    public void Attack1()
    {
        attack.Execute(1);
    }

    public void Attack2()
    {
        attack.Execute(2);
    }

    public void Attack3()
    {
        attack.Execute(3);
    }
}
