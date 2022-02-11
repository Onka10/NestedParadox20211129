using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class EnemyRabbitAgent : Agent
{
    private TempCharacter mainChara;
    [SerializeField] EnemyMoving enemyMoving;
    [SerializeField] EnemyRabbit enemyRabbit;
    [SerializeField] Collider2D attackColl;

    // Start is called before the first frame update
    public override void Initialize()
    {
        mainChara = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        enemyMoving = GetComponent<EnemyMoving>();
        enemyRabbit = GetComponent<EnemyRabbit>();
        attackColl.OnTriggerEnter2DAsObservable().Where(other => CompareTag("MainCharacter")).Subscribe(_ =>
        {
            AddReward(2);
            EndEpisode();
        }).AddTo(this);
    }

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < -3)
        {
            this.transform.position = new Vector3(-10, -1, 0);
        }
        mainChara.transform.position = new Vector3(Random.Range(-10, 30), Random.Range(-1, 4), 0);
        mainChara.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        transform.position = new Vector3(Random.Range(-10, 30), Random.Range(-1, 4), 0);
    }

    /*
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(mainChara.transform.position);
    }
    */

    public override void OnActionReceived(ActionBuffers actions)
    {

        int movingAction = actions.DiscreteActions[0];
        if (movingAction == 1 && enemyMoving.IsGrounded.Value && !enemyRabbit.IsAttacking)
        {
            //+x?????
            enemyMoving.Move(1);            
        }
        else if (movingAction == 2 && enemyMoving.IsGrounded.Value && !enemyRabbit.IsAttacking)
        {
            //-x?????
            enemyMoving.Move(-1);            
        }
        else if (movingAction == 3 && enemyMoving.IsGrounded.Value && !enemyRabbit.IsAttacking)
        {
            //+x???????
            enemyMoving.Jump(1);
        }
        else if (movingAction == 4 && enemyMoving.IsGrounded.Value && !enemyRabbit.IsAttacking)
        {
            //-x???????
            enemyMoving.Jump(-1);
        }
        else if (movingAction == 5 && enemyMoving.IsGrounded.Value && enemyRabbit.CanAttack)
        {
            enemyRabbit.Attack();
        }

        /*
        float distance = (this.transform.position - mainChara.transform.position).magnitude;
        if(distance <= 2)
        {
            Debug.Log("????");
            AddReward(1.5f);
            EndEpisode();
        }
        */
        if (this.transform.position.y < -3)
        {  
            EndEpisode();
        }
        if (mainChara.transform.position.y < -3)
        {
            mainChara.transform.position = new Vector3(Random.Range(-10, 30), Random.Range(-1, 4), 0);
        }


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;
        if (Input.GetKeyDown(KeyCode.D))
        {
            actions[0] = 3;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            actions[0] = 4;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            actions[0] = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            actions[0] = 2;
        }
    }


}
