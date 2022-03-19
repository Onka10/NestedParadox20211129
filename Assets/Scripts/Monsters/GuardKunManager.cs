using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Monsters
{
    public class GuardKunManager : Singleton<GuardKunManager>
    {
        private bool isActive;
        public bool IsActive => isActive;
        private List<GuardKun> guardKuns;

        [SerializeField] int damageReductionRate;
        [SerializeField] GameObject guardKunPrefab;
        // Start is called before the first frame update
        void Start()
        {
            guardKuns = new List<GuardKun>();
            isActive = false;
            //Summon();              
        }

        // Update is called once per frame
        void Update()
        {
            guardKuns.RemoveAll(a => a == null);
            if (guardKuns.Count != 0)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
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
    }
}
