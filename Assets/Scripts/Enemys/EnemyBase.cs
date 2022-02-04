using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class EnemyBase : MonoBehaviour
{
    protected int hp;
    public int Hp { get { return hp; } }
    protected int attackPower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack();

    public abstract void Damaged(int damage);
}
