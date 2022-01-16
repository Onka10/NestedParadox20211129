using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class EnemyRabbitAgent : Agent
{
    private TempCharacter mainChara;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public override void Initialize()
    {
        mainChara = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        if (transform.localPosition.y < -3)
        {
            transform.position = new Vector3(-10, -1, 0);
        }
        mainChara.transform.position = new Vector3(Random.Range(-10, 30), Random.Range(-1, 4), 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float movingPower_x = actions.ContinuousActions[0];
        float movingPower_y = actions.ContinuousActions[1];
        rb.AddForce(new Vector3(movingPower_x, movingPower_y, 0));

        float distance = (transform.position - mainChara.transform.position).magnitude;
        if(distance <= 1.2f)
        {
            AddReward(1);
            EndEpisode();
        }
        if(transform.position.y < -3)
        {
            EndEpisode();
        }
        if(mainChara.transform.position.y < -3)
        {
            EndEpisode();
        }
    }
}
