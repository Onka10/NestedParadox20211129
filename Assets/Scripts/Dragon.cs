using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    float attackTime;
    float attackSpan;
    float DeathSpan;
    float DeathTime;
    int monsterCount;
    [SerializeField] Animator animator;
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = 0;
        attackSpan = 5;
        DeathTime = 0;
        DeathSpan = 20;
        monsterCount = 0;
        GameObject[] monstersArray1 = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] monsterArray2 = GameObject.FindGameObjectsWithTag("GardKun");
        List<GameObject> monsters = new List<GameObject>();
        monsters.AddRange(monstersArray1);
        monsters.AddRange(monsterArray2);
        foreach(GameObject monster in monsters)
        {
            monsterCount += 1;
            GameObject explosion_clone = Instantiate(explosionPrefab);
            explosion_clone.transform.position = monster.transform.position;
            Destroy(monster.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DeathTime += Time.deltaTime;
        if(DeathTime > DeathSpan)
        {
            Destroy(gameObject);
        }

        attackTime += Time.deltaTime;
        if(attackTime > attackSpan)
        {
            attackTime = 0;
            animator.SetTrigger("AttackTrigger");
        }
    }

    public void GenerateFireBall()
    {
        GameObject fireBall_clone = Instantiate(fireBallPrefab);
        fireBall_clone.GetComponent<FireBall>().attackValue = 20 * monsterCount;
        fireBall_clone.transform.position = transform.TransformPoint(new Vector3(0.96f, -0.636f, 0));
    }
}
