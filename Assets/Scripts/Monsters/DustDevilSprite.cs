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
        [SerializeField] float gatheringPower;        

        //エフェクト群
        [SerializeField] GameObject dustDevilRedEffect;

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
            List<GameObject> redEffects = new List<GameObject>();
            destroyedMonsterCount = monsters.Length;
            //モンスターの位置に赤いエフェクトを設置
            foreach(Vector3 monsterPosition in monstersPosition)
            {
                GameObject redEffet_clone = Instantiate(dustDevilRedEffect);
                redEffet_clone.transform.position = monsterPosition;
                redEffects.Add(redEffet_clone);
            }
            //全てのモンスターを破壊
            foreach(GameObject monster in monsters)
            {
                Destroy(monster);
            }

            while(!IsMonsterGathered(monstersPosition))
            {
                foreach (GameObject redEffect in redEffects)
                {
                    Vector3 movingDirection = (transform.position - redEffect.transform.position).normalized;
                    redEffect.GetComponent<Rigidbody2D>().AddForce(movingDirection * gatheringPower);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: this.GetCancellationTokenOnDestroy());
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
            await DestroyAllMonsters();
            Destroy(summonEffect_clone);
            Destroy(summonRedEffect_clone);           
        }

        private bool IsMonsterGathered(Vector3[] monstersPosition)
        {
            foreach(Vector3 monsterPosition in monstersPosition)
            {
                float distance = (transform.position - monsterPosition).magnitude;
                if(distance > 0.1f)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
