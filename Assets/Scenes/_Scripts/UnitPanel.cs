using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disablePanel() // hide the panel when player starts battle
    {
        gameObject.SetActive(false);
    }

    public void Unpause() // game should start paused, so that player can drag units to the field
    {
        Time.timeScale = 1;
    }

    public void shrinkCamera() // shrink the camera to show more of the field and move it to focus on the battle area
    {
        Camera.main.orthographicSize = 6;
        Camera.main.transform.position += new Vector3(-2, 0, 0);
    }
}
