using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Monsters
{
    public class MonsterSprite : MonoBehaviour
    {
        [SerializeField] protected GameObject summonEffect;
        [SerializeField] protected GameObject summonRedEffect;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Vector3 summonEffectScale;
        [SerializeField] protected Vector3 summonEffectPosition;
        [SerializeField] protected Vector3 summonRedEffectScale;
        [SerializeField] protected Vector3 summonPosition;


        public bool IsSummonCompleted;//召喚が完了したらMonsterManagerへ通知
                                      // Start is called before the first frame update
        void Start()
        {

        }

        public Vector3 SetSummonPosition(Vector3 playerPosition, int playerDirection)
        {
            if (playerDirection == 1)
            {
                summonPosition = playerPosition + new Vector3(summonPosition.x, summonPosition.y, summonPosition.z);
            }
            else if (playerDirection == -1)
            {
                summonPosition = playerPosition + new Vector3(-1 * summonPosition.x, summonPosition.y, summonPosition.z);
            }
            return new Vector3(summonPosition.x, summonPosition.y, summonPosition.z);
        }

        public virtual async UniTask SummonAnimation()
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
            Destroy(this.gameObject);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
