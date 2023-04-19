using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomLaserPointer : MonoBehaviour
{
    public static CustomLaserPointer instance;

    public Transform handTransformRight;
    //public Transform handTransformLeft;
    //bool rightRay;
    //bool leftRay;

    RaycastHit hit;
    RaycastHit hitChart;
    float distance = 10;

    private void Awake()
    {
        instance = this;
        //rightRay = Physics.Raycast(handTransformRight.transform.position, handTransformRight.forward, out hit, distance);
        //leftRay= Physics.Raycast(handTransformLeft.transform.position, handTransformLeft.forward, out hit, distance);
    }

    public bool LaserHitModel()
    {
        if (Physics.Raycast(handTransformRight.transform.position, handTransformRight.forward, out hit, distance)) 
        {
            if(hit.collider.gameObject.tag == "model")
            {
                return true;
            }
        }
        //Debug.Log("no hit");
        return false;
    }

    public bool LaserHitChart()
    {
        if (Physics.Raycast(handTransformRight.transform.position, handTransformRight.forward, out hitChart, distance))
        {
            if (hitChart.collider.gameObject.tag == "chart")
            {
                return true;
            }
        }
        //Debug.Log("no hit");
        return false;
    }
    
    public RaycastHit getHit()
    {
        return hit;
    }

    public RaycastHit getHitChart()
    {
        return hitChart;
    }
}
