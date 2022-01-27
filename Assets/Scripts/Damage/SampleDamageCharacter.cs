using UnityEngine;

namespace NestedParadox.Damage{
    //IAttackableを継承します。
    public class SampleDamageCharacter : MonoBehaviour,IAttackable
    {
        public int Hp
        {
            get{return _hp; }
        }
        public int AttackPower
        {
            get{return _attackpower; }
        }

        private int _hp;
        private int _attackpower;

        public void Attack(int Damage)
        {
            _hp -=Damage;
            if(_hp < 0) Dead();
        }

        void Dead()
        {
            //死亡処理
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //TryGetComponentでnullチェック
            //攻撃相手のオブジェクトにインターフェースが継承されていれば攻撃可能
            if(collision.gameObject.TryGetComponent<IAttackable>(out IAttackable attack))
            {
                //インターフェースを継承しているなら攻撃力であるAttackPowerを持っているはずなので
                attack.Attack(attack.AttackPower);
            } 
        }
    }
}