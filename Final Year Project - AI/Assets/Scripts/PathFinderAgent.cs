using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PathFinderAgent : Agent
{
    Rigidbody rBody;
    Vector3 startPos;

    public Transform Target;
    public Transform Tiles;
    public float speed;
    public UI_Handler ui;

    // Start is called before the first frame update
    void Start()
    {
        // Gets RigidBody component
        rBody = GetComponent<Rigidbody>();

        startPos = transform.localPosition;
        Debug.Log("Start Pos: " + startPos);
    }

    public override void OnEpisodeBegin()
    {
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = startPos;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent Positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(Tiles.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        
        // Agent Velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (ui.start == true)
        {
            AddReward(0.15f);
            Vector3 controlSignal = Vector3.zero;
            controlSignal.x = vectorAction[0];
            controlSignal.z = vectorAction[1];
            rBody.AddForce(controlSignal * speed);
            
            float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

            if (distanceToTarget < 1.42f)
            {
                AddReward(2f);
                Debug.Log("Reached Target!");
                EndEpisode();
            }
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
