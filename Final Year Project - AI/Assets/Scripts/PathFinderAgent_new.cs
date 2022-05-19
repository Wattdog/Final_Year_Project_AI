using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;


public class PathFinderAgent_new : Agent
{
    // Starting Position
    Vector3 startPos = new Vector3(0.0f, 1.0f, -5.0f);

    // UI Handler
    public UI_Handler ui;

    // Mask actions
    public bool maskActions = true;

    // Actions for the agent
    const int k_NoAction = 0; // do nothing!
    const int k_Up = 1;
    const int k_Down = 2;
    const int k_Left = 3;
    const int k_Right = 4;

    EnvironmentParameters m_RestParams;

    // Start is called before the first frame update
    void Start()
    {
        // Sets agents position to start position
        transform.position = startPos;
        Debug.Log("Start Pos: " + startPos);
    }

    public override void Initialize()
    {
        m_RestParams = Academy.Instance.EnvironmentParameters;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
    }

    public override void CollectDiscreteActionMasks(DiscreteActionMasker actionMasker)
    {
        // Masks agents action
        if (maskActions)
        {
            var positionX = (int)transform.position.x;
            var positionZ = (int)transform.position.z;
            var maxPosition = (int)m_RestParams.GetWithDefault("gridSize", 5f) - 1;

            if (positionX == 0)
            {
                actionMasker.SetMask(0, new[] { k_Left });
            }

            if (positionX == maxPosition)
            {
                actionMasker.SetMask(0, new[] { k_Right });
            }

            if (positionZ == 0)
            {
                actionMasker.SetMask(0, new[] { k_Down });
            }

            if (positionZ == maxPosition)
            {
                actionMasker.SetMask(0, new[] { k_Up });
            }
        }
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
            // Generates a number between 0 and 4
            var action = Mathf.FloorToInt(vectorAction[0]);

            // Used to determine where the agent will move to
            var targetPos = transform.position;

            // Generated number will be put through switch case to 
            // decide which action has been chosen
            switch (action)
            {
                // Will move the agent to certain position depending on the action
                case k_NoAction:
                    // do nothing
                    break;
                case k_Right:
                    targetPos = transform.position + new Vector3(2.5f, 0, 0f);
                    Debug.Log(gameObject.transform.position);
                    break;
                case k_Left:
                    targetPos = transform.position + new Vector3(-2.5f, 0, 0f);
                    Debug.Log(gameObject.transform.position);
                    break;
                case k_Up:
                    targetPos = transform.position + new Vector3(0f, 0, 2.5f);
                    Debug.Log(gameObject.transform.position);
                    break;
                case k_Down:
                    targetPos = transform.position + new Vector3(0f, 0, -2.5f);
                    Debug.Log(gameObject.transform.position);
                    break;
                default:
                    break;
            }
            // Creates a box collider for the agent
                    var hit = Physics.OverlapBox(targetPos,
                new Vector3(0.5f, 0.5f, 0.5f));

            // Checks to see if the agent has collided with a wall
            if (hit.Where(col => col.gameObject.CompareTag("wall")).ToArray().Length == 0)
            {
                transform.position = targetPos;
                
                // If the goal is reached the agent will be rewarded and will be reset
                if (hit.Where(col => col.gameObject.CompareTag("End")).ToArray().Length == 1)
                {
                    SetReward(20f);
                    EndEpisode();
                }
                // If the agent hits a trap it will be punished and some of the reward will be taken away
                if (hit.Where(col => col.gameObject.CompareTag("Trap")).ToArray().Length == 1)
                {
                    SetReward(-3f);
                    EndEpisode();
                }
            }
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        // Used to test the movement of the agent
        actionsOut[0] = k_NoAction;
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = k_Right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = k_Up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = k_Left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = k_Down;
        }
    }
}
