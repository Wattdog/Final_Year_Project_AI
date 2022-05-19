using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PathFinderAgent : Agent
{
    Rigidbody rBody;
    Vector3 startPos = new Vector3(0.0f, 1.0f, -5.0f);

    public Transform Target;
    public Transform Tiles;
    public float speed;
    public UI_Handler ui;

    // Start is called before the first frame update
    void Start()
    {
        // Gets RigidBody component
        rBody = GetComponent<Rigidbody>();

        transform.position = startPos;
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
        // Checks to see if player has clicked the start button
        // If the start button has been pressed the agent will start
        // to find the route
        if (ui.start == true)
        {
            // Adds reward for every action the agent does
            AddReward(2f);

            // Vector created to hold the 2 actions the agent can do
            // move on x-axis and z-axis
            Vector3 controlSignal = Vector3.zero;
            controlSignal.x = vectorAction[0];
            controlSignal.z = vectorAction[1];

            // Adds force (velocity) to the agent
            rBody.AddForce(controlSignal * speed);
            
            // Calculate the distance between the agent and goal
            float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

            // Used to determine if the agent has reached the goal
            // If it reaches the goal it will be rewarded and will be reset
            if (distanceToTarget < 1.42f)
            {
                AddReward(20f);
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

    void hitTrap()
    {
        // If the agent hits a trap it will be punished
        // the reward will be taken away
        AddReward(-3f);
        Reset();
    }

    void Reset()
    {
        // Resets agent
        EndEpisode();
    }

    void OnTriggerEnter(Collider other)
    {
        // Checks to see if the agent has collided with a trap
        if (other.gameObject.CompareTag("Trap"))
        {
            hitTrap();
            Debug.Log("Hit Trap");
        }
    }
}
