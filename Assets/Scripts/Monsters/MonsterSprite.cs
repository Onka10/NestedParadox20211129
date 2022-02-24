using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MonsterSprite : MonoBehaviour
{
    [SerializeField] GameObject summonEffect;
    [SerializeField] GameObject summonRedEffect;
    [SerializeField] Animator animator;
    [SerializeField] Vector3 summonEffectScale;
    [SerializeField] Vector3 summonEffectPosition;
    [SerializeField] Vector3 summonRedEffectScale;

    public bool IsSummonCompleted;//召喚が完了したらMonsterManagerへ通知
    // Start is called before the first frame update
    void Start()
    {        
    }

    public async UniTask SummonAnimation()
    {        
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
