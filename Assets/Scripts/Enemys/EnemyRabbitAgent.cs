using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class EnemyRabbitAgent: Agent
{
    private TempCharacter mainChara;        
    private EnemyMoving enemyMoving;
    private EnemyRabbit enemyRabbit;    

    // Start is called before the first frame update
    public override void Initialize()
    {
        mainChara = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        enemyMoving = GetComponent<EnemyMoving>();
        enemyRabbit = GetComponent<EnemyRabbit>();
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
        if(movingAction == 1 && enemyMoving.IsGrounded.Value)
        {
            //+x方向へ移動
            enemyMoving.Move(1);
            AddReward(0.0005f);
        }
        else if(movingAction == 2 && enemyMoving.IsGrounded.Value)
        {
            //-x方向へ移動
            enemyMoving.Move(-1);
            AddReward(0.0005f);
        }
        else if(movingAction == 3 && enemyMoving.IsGrounded.Value)
        {
            //+x方向へジャンプ
            enemyMoving.Jump(1);
        }
        else if(movingAction == 4 && enemyMoving.IsGrounded.Value)
        {
            //-x方向へジャンプ
            enemyMoving.Jump(-1);
        }

        float distance = (this.transform.position - mainChara.transform.position).magnitude;
        if(distance <= 2)
        {
            Debug.Log("報酬獲得");
            AddReward(1.5f);
            EndEpisode();
        }
        if(this.transform.position.y < -3)
        {
            AddReward(-1);
            EndEpisode();
        }
        if(mainChara.transform.position.y < -3)
        {
            EndEpisode();
        }
       
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;
        if(Input.GetKeyDown(KeyCode.D))
        {
            actions[0] = 3;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            actions[0] = 4;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            actions[0] = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            actions[0] = 2;
        }
    }


}
