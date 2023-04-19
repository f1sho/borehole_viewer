using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalMovement : MonoBehaviour
{
    public float verticalSpeed = 2.0f;
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;

    void Update()
    {
        
        
        Vector2 thumbstickR = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float rotation = thumbstickR.x;
        float vertical = thumbstickR.y;

        transform.Translate(new Vector3(0, vertical, 0) * Time.deltaTime * verticalSpeed);
        transform.RotateAround(new Vector3(0,0,3f), Vector3.up, rotation * rotationSpeed * Time.deltaTime);

        Vector2 thumbstickL = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float horizontal = thumbstickL.x;
        float verticalL = thumbstickL.y;

        transform.Translate(new Vector3(horizontal, 0, verticalL) * Time.deltaTime * speed);

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(0f, 0f, 0f);
            gameObject.SetActive(true);
        }
    }

}
