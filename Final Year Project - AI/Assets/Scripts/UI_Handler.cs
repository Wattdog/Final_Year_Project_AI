using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Handler : MonoBehaviour
{
    public bool trap;
    public bool noTrap;
    public bool start;

    public Button trapButton;
    public Button noTrapButton;
    public Button startButton;
    public Button switchButton;
    public Text movementSystem;

    // Start is called before the first frame update
    void Start()
    {
        // Sets booleans to false
        trap = false;
        noTrap = false;
        start = false;
    }

    public void startTraining()
    {
        // Starts training
        start = true;
        trapButton.gameObject.SetActive(false);
        noTrapButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        switchButton.gameObject.SetActive(false);
        movementSystem.gameObject.SetActive(false);
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

    public void switchSceneStepbyStep()
    {
        // Loads step-by-step scene
        SceneManager.LoadScene("Step-by-step");
    }

    public void switchSceneContinuous()
    {
        // Loads continuous scene
        SceneManager.LoadScene("Continuous");
    }

    public void reload()
    {
        // reloads current scene
        Debug.Log("Reloading scene...");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void exit()
    {
        // Closes application
        Debug.Log("Closing...");
        Application.Quit();
    }
}
