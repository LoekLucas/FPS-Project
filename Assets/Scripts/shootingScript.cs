using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour
{
    public Camera cam; // Asks for camera

    private Ray ray; // Sets a raycast variable
    private RaycastHit hit; // Sets a varaiable for detecting raycast hits

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If mouse button is pressed down
        {
            ray = cam.ScreenPointToRay(Input.mousePosition); // Sets ray to mouseposition on screen point
            if (Physics.Raycast(ray, out hit)) // If ray hit something
            {
                if (hit.collider.tag.Equals("NPC")) // If object ray hit has tag "NPC"
                {
                    Destroy(hit.collider.gameObject); // Destroy the object
                }
            }
        }


    }
}
