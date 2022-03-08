using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHit : MonoBehaviour
{
    public PathFinderAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = agent.GetComponent<PathFinderAgent>();
    }

    void Reset()
    {
        agent.EndEpisode();
    }

    void hitTrap()
    {
        agent.AddReward(-1);
        Reset();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            hitTrap();
            Debug.Log("Hit Trap");
        }
    }
}
