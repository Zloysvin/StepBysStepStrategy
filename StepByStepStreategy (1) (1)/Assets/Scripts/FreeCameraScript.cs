using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraScript : MonoBehaviour
{


    public float movementSpeed = 10f;
    public float freeLookSensitivity = 0.8f;
    public GameObject cam;

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            FreeMovement();
            cam.GetComponent<SmoothCameraMovement>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            cam.GetComponent<SmoothCameraMovement>().enabled = true;
        }
    }
    public void FreeMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        }
        
    }
   
}
