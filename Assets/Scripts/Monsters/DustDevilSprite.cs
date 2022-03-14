using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Monsters
{
    public class DustDevilSprite : MonsterSprite
    {
        private int destroyedMonsterCount;
        public int DestroyedMonstersCount => destroyedMonsterCount;
        // Start is called before the first frame update
        void Start()
        {
            destroyedMonsterCount = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private async UniTask DestroyAllMonsters()
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            Vector3[] monstersPosition = Array.ConvertAll(monsters, x => x.transform.position);
            destroyedMonsterCount = monsters.Length;
            foreach(Vector3 monsterPosition in monstersPosition)
            {
                GameObject redEffet_clone = Instantiate(summonRedEffect);
                redEffet_clone.transform.position = monsterPosition;
            }
                     
        }

        public override async UniTask SummonAnimation()
        {
            transform.position = summonPosition;
            GameObject summonEffect_clone = Instantiate(summonEffect);
            summonEffect_clone.transform.position = transform.position + summonEffectPosition;
            summonEffect_clone.transform.localScale = summonEffectScale;
            GameObject summonRedEffect_clone = Instantiate(summonRedEffect);
            summonRedEffect_clone.transform.position = transform.position;
            summonRedEffect_clone.transform.localScale = summonRedEffectScale;
            await UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Summon"));

            Destroy(summonEffect_clone);
            Destroy(summonRedEffect_clone);           
        }
    }

}
