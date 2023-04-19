/*
For Oculus Touch and Quest controllers.

This code is a modified version of the wonderful Flight Navigation script by
AncientC https://ancientc.com/ac/ concept by Kevin Mack http://www.kevinmackart.com/
Unity asset store: "Flight Navigation for HTC Vive controller" 
https://assetstore.unity.com/packages/tools/flight-navigation-for-htc-vive-controller-61830

Note: there may be some unnecessary code bits (like "Show Thrust Mockup") that are not needed,
but I left them in, just incase they are of use to you. Also, I don't know what the "FixedJoint"
does, so left it in as well. Hey I'm an artist not a developer! What the heck do I know?

Someone smart can probably figure out how to add a funtion to check if the user is on a Vive or 
Oculus and make one script. (please share if you do!)
-scobot 2019
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightNav_TouchQuest : MonoBehaviour
{
    public GameObject lefthand;
    public GameObject head;

    private float flyingSpeed = 0.8f;
    private bool isFlying = false;

    private void Update()
    {
        checkIfFlying();
        flyIfFlying();
    }

    private void checkIfFlying()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            isFlying = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            isFlying = false;
        }
    }

    private void flyIfFlying()
    {
        if (isFlying == true)
        {
            Vector3 flyDir = lefthand.transform.forward;
            transform.position += flyDir.normalized * flyingSpeed;
        }
    }
}