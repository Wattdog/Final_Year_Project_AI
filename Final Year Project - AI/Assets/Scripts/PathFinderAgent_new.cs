using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.MLAgents;


public class PathFinderAgent_new : Agent
{
    Vector3 startPos;

    public UI_Handler ui;

    public bool maskActions = true;

    const int k_NoAction = 0; // do nothing!
    const int k_Up = 1;
    const int k_Down = 2;
    const int k_Left = 3;
    const int k_Right = 4;

    EnvironmentParameters m_RestParams;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
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
        if (ui.start == true)
        {
            //AddReward(-0.1f);
            var action = Mathf.FloorToInt(vectorAction[0]);

            var targetPos = transform.position;

            switch (action)
            {
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
                    var hit = Physics.OverlapBox(targetPos,
                new Vector3(0.5f, 0.5f, 0.5f));

            if (hit.Where(col => col.gameObject.CompareTag("wall")).ToArray().Length == 0)
            {
                transform.position = targetPos;

                if (hit.Where(col => col.gameObject.CompareTag("End")).ToArray().Length == 1)
                {
                    SetReward(1f);
                    EndEpisode();
                }
                if (hit.Where(col => col.gameObject.CompareTag("Trap")).ToArray().Length == 1)
                {
                    SetReward(-1f);
                    EndEpisode();
                }
            }
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
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
