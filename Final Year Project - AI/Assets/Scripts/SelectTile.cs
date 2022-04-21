using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    private GameObject tile;
 
    public float pos;
    public UI_Handler ui;
    public Material trap;
    public Material noTrap;
    public int limit;
    public int maxLimit;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Gets the current game object the mouse cursor is hovering over
            tile = hit.collider.gameObject;
            tile.GetComponent<Transform>();
            // Gets the transform component and changes the y position 
            // while the mouse is hovering over the game object
            //tile.transform.localPosition = new Vector3(tile.transform.localPosition.x, pos, tile.transform.localPosition.z);
        }
        else
        {
            // Puts the game object back to the original position
            //tile.transform.localPosition = new Vector3(tile.transform.localPosition.x, 0, tile.transform.localPosition.z);
        }


        // Checks to see if the left mouse button has been pressed
        // If it has been pressed it will then check if it has been pressed
        // while the the mouse is hovering over the start or finish tile
        if (Input.GetMouseButtonDown(0) && tile.name != "Start" && tile.name != "Finish")
        {
            // If it hasn't been pressed while over the start or finish tile
            // it will then allow the player to decide whether a tile will
            // have a trap or not
            traps();
        }
    }

    void traps()
    {
        // Checks to see if the trap boolean is set to true
        if (ui.trap == true && maxLimit != limit)
        {
            tile = hit.collider.gameObject;
            tile.transform.GetChild(0).gameObject.SetActive(true);
            tile.tag = "Trap";
            maxLimit++;
            Debug.Log(maxLimit);
        }

        // Checks to see if the noTrap boolean is set to true
        if (ui.noTrap == true && maxLimit <= limit && maxLimit != 0)
        {
            tile = hit.collider.gameObject;
            tile.transform.GetChild(0).gameObject.SetActive(false);
            tile.tag = "No Trap";
            maxLimit--;
            Debug.Log(maxLimit);
        }
    }
}
