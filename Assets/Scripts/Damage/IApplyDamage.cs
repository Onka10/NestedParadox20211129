using UniRx;

interface IApplyDamage
{
    //ダメージを受けるオブジェクトの場合、必ずIApplyDamageを実装する必要があります
    
    //HPが無いと攻撃が通らないバグに繋がるので必要
    int Hp {get;}

    //引数のDamageは攻撃時に攻撃側が与えてください
    void Damaged(int Damage);
}
