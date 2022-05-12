using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class OmniMissile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D coll;
    [SerializeField] Collider2D attackColl;
    [SerializeField] Collider2D bodyColl;

    [SerializeField] int explosionDelayTime;

    [SerializeField] GameObject explosionEffect;

    private bool isGrounded;
    private void Start()
    {
        isGrounded = false;
        coll.OnTriggerEnter2DAsObservable()
            .Where(other => other.gameObject.CompareTag("BaseField") || other.gameObject.CompareTag("MainCharacter"))
            .Subscribe(_ =>
            {
                isGrounded = true;
            })
            .AddTo(this);
        //attackColl.OnCollisionEnter2DAsObservable() ダメージ処理 後で実装
    }

    public async void Shot(Vector3 destination, float shotForce, bool IsExplosionHorizontal)
    {
        Vector3 direction = (destination - transform.position).normalized;
        float rad = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, 90 + rad * 180/Mathf.PI);
        while(!isGrounded && transform.position.x > 0)
        {
            rb.AddForce(direction * shotForce);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        Debug.Log("地面につきました");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.velocity = Vector3.zero;
        bodyColl.enabled = true;
        animator.SetTrigger("MissileExplosionTrigger");
        await UniTask.Delay(explosionDelayTime);        
        GameObject explosionEffect_clone = Instantiate(explosionEffect);       
        explosionEffect_clone.transform.position = transform.position;
        if(IsExplosionHorizontal)
        {
            explosionEffect_clone.transform.localScale = new Vector3(1, 1, 0.25f);
        }
        attackColl.enabled = true;
        await UniTask.Delay(500);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update    
    // Update is called once per frame
    void Update()
    {
        
    }
}
