using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Monsters
{
    public class GuardKunManager : Singleton<GuardKunManager>
    {
        private int count;
        public int Count => count;
        private List<GuardKun> guardKuns;

        [SerializeField] int damageReductionRate;
        [SerializeField] GameObject guardKunPrefab;
        // Start is called before the first frame update
        void Start()
        {
            guardKuns = new List<GuardKun>();
            count = 0;
            //Summon();              
        }

        // Update is called once per frame
        void Update()
        {
            guardKuns.RemoveAll(a => a == null);
            count = guardKuns.Count;
        }

        public void Add(GuardKun guardKun)
        {
            guardKuns.Add(guardKun);
        }

        public void Summon()
        {
            GameObject guardKun_clone = Instantiate(guardKunPrefab);
            guardKuns.Add(guardKun_clone.GetComponent<GuardKun>());
        }

        public void Guard(ref int damage)
        {
            damage = damage * (1 - guardKuns.Count * damageReductionRate);
            int random = Random.Range(0, guardKuns.Count);
            guardKuns[random].Guard();
        }

        public GuardKun SelectQuintetSummonGuardKun()
        {
            int random = Random.Range(0, guardKuns.Count);
            GuardKun selectedGuardKun = guardKuns[random];
            guardKuns.RemoveAt(random);
            return selectedGuardKun;
        }
    }
}
