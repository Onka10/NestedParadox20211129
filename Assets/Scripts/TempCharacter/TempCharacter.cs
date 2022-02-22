using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using NestedParadox.Monsters;
using UniRx.Operators;
using UniRx.Toolkit;
using UniRx.Diagnostics;
using UniRx.InternalUtil;

public class TempCharacter : MonoBehaviour, IApplyDamage
{
    private Rigidbody2D rb;
    [SerializeField] float maxSpeed;
    [SerializeField] float movingPower;
    [SerializeField] float jumpingSpeed;

    private ReactiveProperty<Vector3> currentDirection = new ReactiveProperty<Vector3>();
    public IReadOnlyReactiveProperty<int> CurrentDirection => currentDirection.Select(x => x.x < 0 ? 1 : -1).ToReactiveProperty<int>();

    private AsyncSubject<int> onDamagedAsyncSubject = new AsyncSubject<int>();
    public AsyncSubject<int> OnDamagedAsyncSubject => onDamagedAsyncSubject;

    private Transform myTransform;
    public Transform MyTransform { get { return myTransform; } }

    private bool isLanding;
    private GuardKunManager guardKunManager;

    private int hp;
    public int Hp => hp;

    private ReactiveProperty<int> hp_test = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Hp_test => hp_test;

    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        hp_test.Value = 100;
        guardKunManager = GameObject.Find("GuardKunManager").GetComponent<GuardKunManager>();
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
        currentDirection.Value = myTransform.localScale;
        isLanding = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y == 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
    

    private void FixedUpdate()
    {
       
        float maxSpeed_temp = 0;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            myTransform.localScale = new Vector3(-0.07f, 0.07f, 0.07f);
            currentDirection.Value = myTransform.localScale;
            maxSpeed_temp = maxSpeed;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            myTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
            currentDirection.Value = myTransform.localScale;
            maxSpeed_temp = -1*maxSpeed;
        }
        rb.AddForce(new Vector3(movingPower * (maxSpeed_temp - rb.velocity.x), 0, 0));
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rb.velocity = new Vector3(0, jumpingSpeed, 0);
        }
       
    }

    public void DamagedTest()
    {
        hp_test.Value -= 1;
    }


    public void Damaged(int damage)
    {
        
        if(guardKunManager.IsActive)
        {            
            guardKunManager.Guard(ref damage);
            hp -= damage;
            Debug.Log($"{damage}のダメージを受けました。(ガード成功)");
            return;
        }
        hp -= damage;
        onDamagedAsyncSubject.OnNext(0);
        Debug.Log($"{damage}のダメージを受けました。");
    }
}


