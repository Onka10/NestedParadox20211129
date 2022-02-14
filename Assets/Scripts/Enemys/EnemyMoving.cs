using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class EnemyMoving : MonoBehaviour
{
    private bool isGrounded;
    public bool IsGrounded => isGrounded;

    private bool isFalling;
    public bool IsFalling => isFalling;

    public bool CanMove { get; private set; }

    private int direction;

    [SerializeField] float rayDistance;
    [SerializeField] float ray2AndRay3Distance;
    [SerializeField] Vector2 ray2;
    [SerializeField] Vector2 ray3;
    [SerializeField] Vector3 jumpingSpeed;
    [SerializeField] float movingSpeed;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject enemyUI;
    [SerializeField] Collider2D bodyColl;
    [SerializeField] GameObject respawnEffect;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        isFalling = false;
        CanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, targetLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, ray2, ray2AndRay3Distance, targetLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, ray3, ray2AndRay3Distance, targetLayer);        
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            isGrounded = true;            
        }
        else
        {
            isGrounded = false;           
        }

        //????????????????
        if(isGrounded && !isFalling)
        {
            CanMove = true;
        }
        else
        {
            CanMove = false;
        }
    }


    public  void Jump(int direction)
    {
        if(direction == 1)
        {            
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = jumpingSpeed;
        }
        else
        {            
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector3(jumpingSpeed.x * -1,jumpingSpeed.y, jumpingSpeed.z);
        }
    }

    public void Move(int direction)
    {
        if(direction == 1)
        {            
            transform.localScale = new Vector3(-1, 1, 1);           
            rb.velocity = new Vector3(movingSpeed, 0, 0);
        }
        else
        {            
            transform.localScale = new Vector3(1, 1, 1);            
            rb.velocity = new Vector3(-1 * movingSpeed, 0, 0);
        }
    }

    public async void OnFell() //??????????
    {
        transform.position = new Vector3(transform.position.x, -2.9f, 0);
        isFalling = true;
        enemyUI.transform.localScale = new Vector3(0, 0, 0);
        rb.isKinematic = true;        
        rb.velocity = Vector3.zero;
        bodyColl.enabled = false;
        GameObject respawnEffect_clone = Instantiate(respawnEffect, transform.position, Quaternion.identity);
        respawnEffect_clone.transform.SetParent(transform);
        await UniTask.Delay(1000);
        GameObject[] baseFields = GameObject.FindGameObjectsWithTag("BaseField");
        float distance = 10000;
        Vector3 nearestPlace = new Vector3();
        foreach(GameObject baseField in baseFields)
        {
            float distance_temp = (transform.position - baseField.transform.position).magnitude;
            if(distance_temp < distance)
            {
                distance = distance_temp;
                nearestPlace = baseField.transform.position;
            }
        }
        Vector3 respawnPosition = new Vector3(nearestPlace.x, nearestPlace.y + 5, nearestPlace.z);        
        while((transform.position - respawnPosition).magnitude > 0.1f)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, respawnPosition.x, 0.1f),
                                             Mathf.Lerp(transform.position.y, respawnPosition.y, 0.1f),
                                             Mathf.Lerp(transform.position.z, respawnPosition.z, 0.1f));
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);                                            
        }        
        isFalling = false;
        enemyUI.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        bodyColl.enabled = true;
        Destroy(respawnEffect_clone);
    }

}
