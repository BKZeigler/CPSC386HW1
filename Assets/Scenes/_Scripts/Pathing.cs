using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

//2 TODOS BELOW:

public class Pathing : MonoBehaviour
{

    //Different Possible Hexagonal Movements
    Vector3 TopLeft = new Vector3(-0.5f, 0.75f, 0);
    Vector3 Left = new Vector3(-1, 0, 0);
    Vector3 BottomLeft = new Vector3(-0.5f, -0.75f, 0);
    Vector3 BottomRight = new Vector3(0.5f, -0.75f, 0);
    Vector3 Right = new Vector3(1, 0, 0);
    Vector3 TopRight = new Vector3(0.5f, 0.75f, 0);

    //Stores current position of the object
    public Vector3 currentPosition = new Vector3(5, 0, 0);

    //Stores target position of the object
    public Vector3 targetPosition = new Vector3(0, 0, 0);
    //fetches the target postion of other object
    void FetchTargetPosition()
    {
        GameObject targetObject = GameObject.Find("Ally1"); //finds the target object by tag
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position; 
            Debug.Log("Target position updated: " + targetPosition); //logs the updated target position
        }
    }

    void FetchCurrentPosition()
    {
        currentPosition = transform.position; //updates the current position of the object
        Debug.Log("Current position updated: " + currentPosition); //logs the updated current position
    }

    //Stores movement vector
    public Vector3 movement = new Vector3(0, 0, 0);

    IEnumerator TargetPathing(float seconds)
    {
        //TODO: SWITCH TO A CONDITION SO THAT UNIT STOPS WHEN IN RANGE OF TARGET
        //TODO: FIX BUG THAT MAKES UNIT ALWAYS GO LEFT ON START OF GAME
        while (true) //infinite loop to continuously update movement towards target
        {
        //calculates the distance between current and target position
           Vector3 DistanceDifference = targetPosition - currentPosition;

        //decides which direction to move based on the distanc difference
            if (DistanceDifference.x > 0 && DistanceDifference.y > 0)
            {
                movement = TopRight;
                DistanceDifference -= TopRight; //update distance difference to reflect the movement
            }
            else if (DistanceDifference.x < 0 && DistanceDifference.y > 0)
            {
                movement = TopLeft;
                DistanceDifference -= TopLeft; //update distance difference to reflect the movement
            }
            else if (DistanceDifference.x < 0 && DistanceDifference.y < 0)
            {
                movement = BottomLeft;
                DistanceDifference -= BottomLeft; //update distance difference to reflect the movement
            }
            else if (DistanceDifference.x > 0 && DistanceDifference.y < 0)
            {
                movement = BottomRight;
                DistanceDifference -= BottomRight; //update distance difference to reflect the movement
            }
            else if (DistanceDifference.x > 0)
            {
                movement = Right;
                DistanceDifference -= Right; //update distance difference to reflect the movement
            }
            else if (DistanceDifference.x < 0)
            {
                movement = Left;
                DistanceDifference -= Left; //update distance difference to reflect the movement
            }
            else
            {
                movement = new Vector3(0,0,0); //if the target is at the same position, do not move
            }

        transform.position += movement; //update position
        yield return new WaitForSeconds(seconds); //wait for speficied amount of time
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TargetPathing(1f)); //performs move logic with given time delay (fasters units smaller delay)
    }

    // Update is called once per frame
    void Update()
    {
        FetchTargetPosition();
        FetchCurrentPosition();
    }
}
