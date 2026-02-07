using UnityEditor;
using UnityEngine;

public class Pathing : MonoBehaviour
{

    //Stores current position of the object
    public Vector3 currentPosition = new Vector3(5, 0, 0);

    //Stores movement vector
    public Vector3 movement = new Vector3(0, 0, 0);

    //Move Function
    public void Move(Vector3 movement)
    {
        transform.position += movement;
        System.Threading.Thread.Sleep(1000); //unit sleeps for 1 second
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = new Vector3(-0.5f,0.75f,0); //Move the unit top-left
        Move(movement);
        movement = new Vector3(-1,0,0); //Move the unit left
        Move(movement);
        movement = new Vector3(-0.5f,-0.75f,0); //Move the unit bottom-left
        Move(movement);
        movement = new Vector3(-0.5f,-0.75f,0); //Move the unit bottom-right
        Move(movement);
        movement = new Vector3(1,0,0); //Move the unit right
        Move(movement);
        movement = new Vector3(0.5f,0.75f,0); //Move the unit top-right
        Move(movement);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
