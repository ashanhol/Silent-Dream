using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionButton : MonoBehaviour
{
    GameObject instructionsObject;
    public Button instructionButton;

    void Start()
    {
        //The actual instructions
        instructionsObject = GameObject.FindGameObjectWithTag("Instructions");

    }

    void Update()
    { 
        Time.timeScale = 1; 
    }

    //Make Instructions visible
    void OnMouseDown()
    {
        instructionsObject.GetComponent<SpriteRenderer>().enabled = true;
        instructionButton.GetComponent<Image>().enabled = true;
        instructionButton.GetComponentInChildren<Text>().enabled = true;

    }
    
    //Hide instructions
    public void GetRidOfInstructions() {

        instructionsObject.GetComponent<SpriteRenderer>().enabled = false;
        instructionButton.GetComponent<Image>().enabled = false;
        instructionButton.GetComponentInChildren<Text>().enabled = false;


    }
}
