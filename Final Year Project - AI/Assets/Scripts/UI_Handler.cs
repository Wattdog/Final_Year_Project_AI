using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public bool trap;
    public bool noTrap;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        trap = false;
        noTrap = false;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTraining()
    {
        // Starts training
        start = true;
    }

    public void setTrap()
    {
        // Sets Trap to true & noTrap to false
        // This will allow the player to add traps
        // to the tiles they have chosen
        trap = true;
        noTrap = false;
        
        Debug.Log("noTrap: " + noTrap);
        Debug.Log("trap: " + trap);
    }

    public void setNoTrap()
    {
        // Sets Trap to false & noTrap to true
        // This will allow the player to remove traps
        // from the tiles they have chosen
        trap = false;
        noTrap = true;
        
        Debug.Log("trap: " + trap);
        Debug.Log("noTrap: " + noTrap);
    }
}
