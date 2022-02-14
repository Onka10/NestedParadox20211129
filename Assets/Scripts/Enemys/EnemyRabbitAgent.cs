using System.Collections;
using System;
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
    private float maxStepTimeCount;
    private List<Vector2> fieldPositions;

    // Start is called before the first frame update
    public override void Initialize()
    {
        mainChara = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        /*
        mainChara.transform.position = new Vector3(UnityEngine.Random.Range(-10, 30), UnityEngine.Random.Range(-1, 4), 0);
        mainChara.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        */      
    }

    public override void OnEpisodeBegin()
    {
        //enemyMoving.transform.position = new Vector3(UnityEngine.Random.Range(-10, 30), UnityEngine.Random.Range(-1, 4), 0);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(enemyMoving.IsGrounded);        
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        /*
        maxStepTimeCount += Time.deltaTime;
        if (maxStepTimeCount > 60)
        {
            maxStepTimeCount = 0;
            mainChara.transform.position = new Vector3(UnityEngine.Random.Range(-10, 30), UnityEngine.Random.Range(-1, 4), 0);
            mainChara.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        */

        float distanceToTarget = (enemyMoving.transform.position - mainChara.transform.position).magnitude;
        if (distanceToTarget < 2 && enemyMoving.CanMove && !enemyRabbit.IsAttacking)
        {
            enemyRabbit.Attack();
            return;
        }

        int movingAction = actions.DiscreteActions[0];
        if (movingAction == 1 && enemyMoving.CanMove && !enemyRabbit.IsAttacking)
        {
            //+x?????
            enemyMoving.Move(1);
        }
        else if (movingAction == 2 && enemyMoving.CanMove && !enemyRabbit.IsAttacking)
        {
            //-x?????
            enemyMoving.Move(-1);
        }
        else if (movingAction == 3 && enemyMoving.CanMove && !enemyRabbit.IsAttacking)
        {
            //+x???????
            enemyMoving.Jump(1);
        }
        else if (movingAction == 4 && enemyMoving.CanMove && !enemyRabbit.IsAttacking)
        {
            //-x???????
            enemyMoving.Jump(-1);
        }

        if(enemyMoving.transform.position.y < -3)
        {
            enemyMoving.OnFell();
        }

        
        //AddReward(-0.0003f);

        float distance = (enemyMoving.transform.position - mainChara.transform.position).magnitude;
        if (distance <= 2 && enemyMoving.IsGrounded)
        {
            //AddReward(2);
            //EndEpisode();
        }

        if (enemyMoving.transform.position.y < -3)
        {
            //AddReward(-0.5f);
            //EndEpisode();
        }

        /*
        if (mainChara.transform.position.y < -3)
        {
            mainChara.transform.position = new Vector3(UnityEngine.Random.Range(-10, 30), UnityEngine.Random.Range(-1, 4), 0);
        }
        */


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
